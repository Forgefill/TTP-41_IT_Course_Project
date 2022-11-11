# Web Api with mongoDB

Для роботи з MongoDB розроблено тип даних Column, що має інформацію про тип даних в колонці, ім'я колонки, стандартне значення і елементи в колонці. Від нього наслідуються класи EnumColumn, EmailColumn, StringColumn, CharColumn, IntegerColumn, RealColumn, що є представленням відповідних типів даних, що зберігаються в базу даних MongoDB.
  До проекту завантажимо офіційний driver mongo для .NET, MongoDB.Driver. Встановимо фреймворк для роботи з JSON в .NET - Newtonsoft. Створено класи ColumnConverter i ColumnSpecifiedConcreteClassConverter, що серіалізують модель даних в JSON, переглянити їх код можна за [посиланням.](https://github.com/Forgefill/TTP-41_IT_Course_Project/tree/master/MongoWebApi/JsonHelpers)
