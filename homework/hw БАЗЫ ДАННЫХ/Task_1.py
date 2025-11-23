class Vehicle:
    def __init__(self, make: str, model: str, odometer: int = 0):
        if not make or not make.strip():
            raise ValueError("Марка не может быть пустой")
        if not model or not model.strip():
            raise ValueError("Модель не может быть пустой")
        if odometer < 0:
            raise ValueError("Пробег не может быть отрицательным числом")

        self.make = make
        self.model = model
        self.odometer = odometer

    def drive(self, distance: int) -> bool:
        if distance <= 0:
            print("Расстояние должно быть положительным числом")
            return False

        self.odometer += distance
        print(f"{self.make} {self.model} проехала {distance} км.")
        return True

    def __str__(self) -> str:
        return f"Марка: {self.make}, Модель: {self.model}, Пробег: {self.odometer} км"


if __name__ == "__main__":
    car1 = Vehicle("БМВ", "E30", 19330)
    car2 = Vehicle("Нисан", "X-Trail", 252000)
    car3 = Vehicle("Шкода", "Октавиа III", 220800)

    print(car1)
    print(car2)
    print(car3)
    print()

    car1.drive(41023)
    car2.drive(12453)
    car3.drive(86326)
    
    print()
    print("Пробег после поездок:")
    print(car1)
    print(car2)
    print(car3)
