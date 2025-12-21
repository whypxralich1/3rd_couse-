from django.shortcuts import render, redirect, get_object_or_404
from django.contrib.auth import authenticate, login, logout
from django.contrib import messages
from django.contrib.auth.decorators import login_required
from django.views.decorators.http import require_POST
from django.http import HttpResponseForbidden
from .forms import LoginForm
from .models import Order, Invoice


def index(request):
    my_lists = [("/secure/order/list/", "Order: мои объекты"), ("/secure/invoice/list/", "Invoice: мои объекты")]
    return render(request, "index.html", {"my_lists": my_lists, "domain_desc": "Заказы/Счета — прямой доступ к чужим ресурсам"})


def login_view(request):
    if request.method == "POST":
        form = LoginForm(request.POST)
        if form.is_valid():
            user = authenticate(request, username=form.cleaned_data["username"], password=form.cleaned_data["password"])
            if user:
                login(request, user)
                messages.success(request, "OK")
                return redirect("index")
            messages.error(request, "Неверные данные")
    else:
        form = LoginForm()
    return render(request, "login.html", {"form": form})


def logout_view(request):
    logout(request)
    messages.info(request, "Вышли")
    return redirect("index")


@login_required
def order_list(request):
    objs = Order.objects.filter(owner=request.user).order_by("-id")
    return render(request, "ordersapp/order_list.html", {"objects": objs})


@login_required
def order_detail_vuln(request):
    obj_id = request.GET.get("id")
    obj = get_object_or_404(Order, id=obj_id)
    if obj.owner != request.user:
        return HttpResponseForbidden()
    return render(request, "ordersapp/order_detail.html", {"obj": obj, "mode": "vuln_query"})


@login_required
def order_detail_secure(request, obj_id):
    obj = get_object_or_404(Order, id=obj_id, owner=request.user)
    return render(request, "ordersapp/order_detail.html", {"obj": obj, "mode": "secure"})


@login_required
def order_detail_vuln_path(request, obj_id):
    obj = get_object_or_404(Order, id=obj_id)
    if obj.owner != request.user:
        return HttpResponseForbidden()
    return render(request, "ordersapp/order_detail.html", {"obj": obj, "mode": "vuln_path"})


@require_POST
@login_required
def order_update_vuln(request, obj_id):
    obj = get_object_or_404(Order, id=obj_id)
    if obj.owner != request.user:
        return HttpResponseForbidden()
    if 'title' in request.POST:
        setattr(obj, 'title', request.POST['title'])
    obj.save()
    return redirect("index")


@login_required
def invoice_list(request):
    objs = Invoice.objects.filter(owner=request.user).order_by("-id")
    return render(request, "ordersapp/invoice_list.html", {"objects": objs})


@login_required
def invoice_detail_vuln(request):
    obj_id = request.GET.get("id")
    obj = get_object_or_404(Invoice, id=obj_id)
    if obj.owner != request.user:
        return HttpResponseForbidden()
    return render(request, "ordersapp/invoice_detail.html", {"obj": obj, "mode": "vuln_query"})


@login_required
def invoice_detail_secure(request, obj_id):
    obj = get_object_or_404(Invoice, id=obj_id, owner=request.user)
    return render(request, "ordersapp/invoice_detail.html", {"obj": obj, "mode": "secure"})


@login_required
def invoice_detail_vuln_path(request, obj_id):
    obj = get_object_or_404(Invoice, id=obj_id)
    if obj.owner != request.user:
        return HttpResponseForbidden()
    return render(request, "ordersapp/invoice_detail.html", {"obj": obj, "mode": "vuln_path"})


@require_POST
@login_required
def invoice_update_vuln(request, obj_id):
    obj = get_object_or_404(Invoice, id=obj_id)
    if obj.owner != request.user:
        return HttpResponseForbidden()
    if 'title' in request.POST:
        setattr(obj, 'title', request.POST['title'])
    obj.save()
    return redirect("index")