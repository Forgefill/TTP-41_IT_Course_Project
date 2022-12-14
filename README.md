# DatabaseManagementSystem

## Основні вимоги до структури бази

Основні вимоги щодо структури бази:
* Кількість таблиць принципово необмежена (реляції між таблицями не враховуються).
* Кількість полів та кількість записів у кожній таблиці також принципово не обмежені.

Потрібно забезпечити підтримку (для полів у таблицях) наступних типів:
* integer;
* real;
* char;
* string;
* email;
* Enum.

Потрібно реалізувати функціональну підтримку для:
* Створення бази даних.
* Створення та знищення таблиці з бази.
* Перегляду та редагування рядків таблиці.
* Збереження табличної бази на диску та, навпаки, зчитування її з диску.
* Видалення повторюванних рядків.

## Діаграма прецедентів

![Use-Case stage 0](img/UseCase0.png?raw=true)


## Етапи проекту

* [Використання UML при проектуванні та специфікації програмних систем](https://github.com/Forgefill/TTP-41_IT_Course_Project/blob/master/docs/stage%204.md)
* [Розробка локальної версії СУТБД](https://github.com/Forgefill/TTP-41_IT_Course_Project/blob/master/docs/stage%201-2.md)
* [REST web-сервіси](https://github.com/Forgefill/TTP-41_IT_Course_Project/blob/master/docs/stage3.md)
* [Розробка OpenApi Specification](https://github.com/Forgefill/TTP-41_IT_Course_Project/blob/master/docs/stage3.md)
* [Asp.Net Web Api](https://github.com/Forgefill/TTP-41_IT_Course_Project/blob/master/docs/stage3.md)
* [Asp.Net MVC](https://github.com/Forgefill/TTP-41_IT_Course_Project/blob/master/docs/stage5.md)
* [MongoDB + webApi](https://github.com/Forgefill/TTP-41_IT_Course_Project/blob/master/docs/stage6.md)
