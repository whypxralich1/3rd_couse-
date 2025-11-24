class Person:
    def __init__(self, first_name: str, last_name: str, age: int, phones=None):
        self.first_name = first_name
        self.last_name = last_name
        self.age = age
        self.phones = phones or []  # если переданных телефонов нет, создаём пустой список

    def add_phone(self, phone):
        self.phones.append(phone)

    def __str__(self):
        return f"{self.first_name} {self.last_name}, {self.age} лет"


class Phone:
    def __init__(self, model: str, operator: str, owner: Person):
        self.model = model
        self.operator = operator
        self.owner = owner

    def __str__(self):
        return f"Телефон модели {self.model} ({self.operator})"


class PhoneBook:
    def __init__(self):
        self.contacts = []

    def add_contact(self, person: Person):
        self.contacts.append(person)

    def show_contacts(self):
        if self.contacts:
            print("Мои контакты:")
            for contact in self.contacts:
                print(contact)
                for phone in contact.phones:
                    print(f"\t{phone}")
        else:
            print("Телефонная книга пуста.")

    def search_by_first_name(self, first_name: str):
        result = [contact for contact in self.contacts if contact.first_name.lower() == first_name.lower()]
        if result:
            print(f"Контакты с именем '{first_name}':")
            for r in result:
                print(r)
        else:
            print(f"Контакт с таким именем не найден.")


person1 = Person(first_name="Илья", last_name="Котлов", age=20)
person2 = Person(first_name="Павел", last_name="Бураков", age=20)

phone1 = Phone(model="iPhone 11", operator= "МТС", owner=person1)
phone2 = Phone(model="Samsung Galaxy S20", operator="TELE 2", owner=person2)

person1.add_phone(phone1)
person2.add_phone(phone2)

book = PhoneBook()
book.add_contact(person1)
book.add_contact(person2)

book.show_contacts()
book.search_by_first_name("Иван")