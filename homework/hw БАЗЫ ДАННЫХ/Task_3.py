class Animal:
    def __init__(self, species, weight):
        self.species = species
        self.weight = weight

    def description(self):
        return f"Это {self.species}, массой {self.weight:.2f} кг."

class Dog(Animal):
    def __init__(self, species, weight, hair_length):
        super().__init__(species, weight)
        self.hair_length = hair_length

    def description(self):
        base_desc = super().description()
        return f"{base_desc} его шерсть длиной {self.hair_length} см."

class Cat(Animal):
    def __init__(self, species, weight, color):
        super().__init__(species, weight)
        self.color = color

    def description(self):
        base_desc = super().description()
        return f"{base_desc} Окрас кота — {self.color}."

dog = Dog('Собака', 25.5, 10)
cat = Cat('Кошка', 4.2, 'Черепаховый')

# Вывод описания
print(dog.description())
print(cat.description())