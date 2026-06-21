# FitnesCenter REST API

## Технологии
- **Язык:** C# (.NET 8)
- **Фреймворк:** ASP.NET Core Web API
- **База данных:** PostgreSQL
- **ORM:**  Entity Framework Core 8
- **Хранилище:** In-Memory (ConcurrentDictionary)
- **Документация:** Swagge/OpenAPI

## Структура проекта
FitnesCenter/
├── FitnesCenter.API/ # Контроллеры, DTO, Middleware
├── FitnesCenter.Application/ # Сервисы (бизнес-логика)
├── FitnesCenter.Domain/ # Сущности, интерфейсы
├── FitnesCenter.Infrastructure/ # Репозитории (EF Core + In-Memory)
└── FitnesCenter.Shared/ # Enums, Exceptions

## Запуск
```bash
### Требования
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)

### Настройка базы данных

1. **Создай базу данных:**
```sql
CREATE DATABASE "FitnesCenter";

2.Обнови строку подключения в appsettings.json:
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=FitnesCenter;Username=postgres;Password=ТВОЙ_ПАРОЛЬ"
  }
}
Запуск
# Восстановить зависимости
dotnet restore

# Применить миграции (создаст таблицы)
dotnet ef database update --project FitnesCenter.Infrastructure --startup-project FitnesCenter.API

# Запустить API
dotnet run --project FitnesCenter.API

## API Эндпоинты

### Клиенты (`/api/clients`)

| Метод | Путь | Описание |
|-------|------|----------|
| POST | `/api/clients` | Создать клиента |
| GET | `/api/clients` | Получить всех клиентов |
| GET | `/api/clients/{id}` | Получить клиента по ID |
| GET | `/api/clients/{id}/detail` | Детальная информация (с тренером, шкафчиком, услугами) |
| PUT | `/api/clients/{id}` | Обновить данные клиента |
| PATCH | `/api/clients/{id}/status` | Активировать/деактивировать клиента |
| POST | `/api/clients/{clientId}/trainer/{trainerId}` | Назначить тренера клиенту |
| POST | `/api/clients/{clientId}/locker/{lockerId}` | **Назначить шкафчик клиенту** |
| POST | `/api/clients/{clientId}/additionalServices/{serviceId}` | **Добавить услугу клиенту** |

### Тренеры (`/api/trainers`)

| Метод | Путь | Описание |
|-------|------|----------|
| POST | `/api/trainers` | Создать тренера |
| GET | `/api/trainers` | Получить всех тренеров |
| GET | `/api/trainers/{id}/detail` | Детальная информация (со списком клиентов) |
| PUT | `/api/trainers/{id}` | Обновить данные тренера |
| PATCH | `/api/trainers/{id}/status` | Изменить статус тренера |

## Шкафчики
| Метод | Путь | Описание |
|-------|------|----------|
| GET | `/api/lockers` | **Получить все шкафчики со статусом** |


### API Эндпоинты — Услуги

| Метод | Путь | Описание |
|-------|------|----------|
| GET | `/api/additionalServices` | **Получить все услуги** |
| GET | `/api/additionalServices/{id}` | **Получить услугу + список клиентов** |


##Тестовые данные

### Тренеры

| ID | Имя | Статус |
|----|-----|--------|
| `11111111-1111-1111-1111-111111111111` | Иванов Алексей Сергеевич | WORKING |
| `44444444-4444-4444-4444-444444444444` | Петрова Мария Ивановна | WORKING |

### Клиенты

| ID | Имя | Тренер |
|----|-----|--------|
| `22222222-2222-2222-2222-222222222222` | Петров Иван Алексеевич | Иванов Алексей |
| `33333333-3333-3333-3333-333333333333` | Сидоров Петр Иванович | Нет |


### Услуги

| ID | Название | Цена |
|----|----------|------|
| SOLARIUM | Солярий | 400 |
| POOL | Бассейн | 200 |
| SAUNA | Сауна | 0 |
| CRYOSAUNA | Криосауна | 1000 |
| CROSSFIT | Кроссфит | 500 |

### Шкафчики
20 шкафчиков с номерами от 1 до 20 (все свободны).


## 🔧 Коды ответов

| Код | Описание |
|-----|----------|
| 200 | Успешный запрос |
| 201 | Ресурс создан |
| 204 | Успешный запрос (без содержимого) |
| 400 | Ошибка валидации |
| 404 | Ресурс не найден |
| 409 | Конфликт (бизнес-правило нарушено) |
| 500 | Внутренняя ошибка сервера |


Автор
Студент: Васильев Кирилл Андреевич
Группа: Исип-313
Дата: Июнь 2026