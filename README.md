# Create DB

```postgresql
-- Подключение Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres;

-- (2) Таблица ролей
CREATE TABLE roles (
    id   INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    role VARCHAR(100)                 NOT NULL
);

-- (3) Таблица пользователей
CREATE TABLE users (
    id        INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    surname   VARCHAR(100)            NOT NULL,
    name      VARCHAR(100)            NOT NULL,
    otchestvo VARCHAR(100),
    role_id   INT                     NOT NULL
               REFERENCES roles(id)
                 ON UPDATE CASCADE
                 ON DELETE RESTRICT,
    login     VARCHAR(100)            NOT NULL UNIQUE,
    password  VARCHAR(100)            NOT NULL,
    count     INT                     NOT NULL DEFAULT 0,
    active    BOOLEAN                 NOT NULL DEFAULT TRUE,
    date      TIMESTAMP WITHOUT TIME ZONE
);

-- (4) Категории номеров
CREATE TABLE room_categories (
    category_id   INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    category_name VARCHAR(100)                 NOT NULL
);

-- (5) Этажи
CREATE TABLE floors (
    floor_id   INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    floor_name VARCHAR(100)                 NOT NULL
);

-- (6) Статусы номеров
CREATE TABLE room_statuses (
    status_id   INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    status_name VARCHAR(100)                 NOT NULL
);

-- (7) Номерной фонд
CREATE TABLE room_stock (
    room_number INT                     PRIMARY KEY,
    category_id INT                     NOT NULL
                 REFERENCES room_categories(category_id)
                   ON UPDATE CASCADE
                   ON DELETE RESTRICT,
    floor_id    INT                     NOT NULL
                 REFERENCES floors(floor_id)
                   ON UPDATE CASCADE
                   ON DELETE RESTRICT,
    status_id   INT                     NOT NULL
                 REFERENCES room_statuses(status_id)
                   ON UPDATE CASCADE
                   ON DELETE RESTRICT
);

-- (8) Постояльцы (гости)
CREATE TABLE guests (
    guest_id    INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    room_number INT                     NOT NULL
                 REFERENCES room_stock(room_number)
                   ON UPDATE CASCADE
                   ON DELETE RESTRICT,
    client_id   INT                     NOT NULL
                 REFERENCES users(id)
                   ON UPDATE CASCADE
                   ON DELETE RESTRICT,
    date_entry  DATE                    NOT NULL,
    date_exit   DATE                    NOT NULL
);

```
Test data
```postgresql
-- (1) Создать базу (если ещё нет) и переключиться на неё:
-- CREATE DATABASE hotel;
-- В DBeaver в дереве правой кнопкой → Set Active → hotel

-- (2) Таблица ролей
CREATE TABLE roles (
    id   INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    role VARCHAR(100)                 NOT NULL
);

-- (3) Таблица пользователей
CREATE TABLE users (
    id        INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    surname   VARCHAR(100)            NOT NULL,
    name      VARCHAR(100)            NOT NULL,
    otchestvo VARCHAR(100),
    role_id   INT                     NOT NULL
               REFERENCES roles(id)
                 ON UPDATE CASCADE
                 ON DELETE RESTRICT,
    login     VARCHAR(100)            NOT NULL UNIQUE,
    password  VARCHAR(100)            NOT NULL,
    count     INT                     NOT NULL DEFAULT 0,
    active    BOOLEAN                 NOT NULL DEFAULT TRUE,
    date      TIMESTAMP WITHOUT TIME ZONE
);

-- (4) Категории номеров
CREATE TABLE room_categories (
    category_id   INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    category_name VARCHAR(100)                 NOT NULL
);

-- (5) Этажи
CREATE TABLE floors (
    floor_id   INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    floor_name VARCHAR(100)                 NOT NULL
);

-- (6) Статусы номеров
CREATE TABLE room_statuses (
    status_id   INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    status_name VARCHAR(100)                 NOT NULL
);

-- (7) Номерной фонд
CREATE TABLE room_stock (
    room_number INT                     PRIMARY KEY,
    category_id INT                     NOT NULL
                 REFERENCES room_categories(category_id)
                   ON UPDATE CASCADE
                   ON DELETE RESTRICT,
    floor_id    INT                     NOT NULL
                 REFERENCES floors(floor_id)
                   ON UPDATE CASCADE
                   ON DELETE RESTRICT,
    status_id   INT                     NOT NULL
                 REFERENCES room_statuses(status_id)
                   ON UPDATE CASCADE
                   ON DELETE RESTRICT
);

-- (8) Постояльцы (гости)
CREATE TABLE guests (
    guest_id    INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    room_number INT                     NOT NULL
                 REFERENCES room_stock(room_number)
                   ON UPDATE CASCADE
                   ON DELETE RESTRICT,
    client_id   INT                     NOT NULL
                 REFERENCES users(id)
                   ON UPDATE CASCADE
                   ON DELETE RESTRICT,
    date_entry  DATE                    NOT NULL,
    date_exit   DATE                    NOT NULL
);

```
