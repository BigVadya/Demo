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
-- 1) Роли
INSERT INTO roles (role) VALUES
  ('Admin'),
  ('Receptionist'),
  ('Client');

-- 2) Пользователи
-- Укажем count, active и дату последнего изменения (например, дату создания):
INSERT INTO users (surname, name, otchestvo, role_id, login, password, count, active, date) VALUES
  ('Smirnov',  'Pavel',   'Sergeevich',    1, 'admin',  'admin',  0, TRUE, '2025-05-01 10:00'),
  ('Petrova',  'Elena',   'Igorevna',      2, 'e.petrova',  'secret456', 0, TRUE, '2025-05-02 11:15'),
  ('Ivanov',   'Ivan',    'Ivanovich',     3, 'i.ivanov',   'qwerty',    0, TRUE, '2025-05-03 12:30'),
  ('Sidorova', 'Anna',    NULL,            3, 'a.sidorova', 'zxcvbn',    0, FALSE,'2025-05-04 13:45');

-- 3) Категории номеров
INSERT INTO room_categories (category_name) VALUES
  ('Single'),
  ('Double'),
  ('Suite');

-- 4) Этажи
INSERT INTO floors (floor_name) VALUES
  ('1st Floor'),
  ('2nd Floor'),
  ('3rd Floor');

-- 5) Статусы номеров
INSERT INTO room_statuses (status_name) VALUES
  ('Available'),
  ('Occupied'),
  ('Maintenance');

-- 6) Номерной фонд
INSERT INTO room_stock (room_number, category_id, floor_id, status_id) VALUES
  (101, 1, 1, 1),
  (102, 1, 1, 1),
  (103, 1, 1, 1),
  (201, 2, 2, 1),
  (202, 2, 2, 1),
  (203, 2, 2, 1),
  (301, 3, 3, 1),
  (302, 3, 3, 1),
  (303, 3, 3, 1);

-- 7) Постояльцы (гости)
INSERT INTO guests (room_number, client_id, date_entry, date_exit) VALUES
  (101,  3, '2025-05-05', '2025-05-10'),
  (202,  4, '2025-05-06', '2025-05-12'),
  (301,  3, '2025-05-08', '2025-05-15');

-- 8) Обновить статусы занятых комнат на "Occupied" (status_id = 2)
UPDATE room_stock
SET status_id = 2
WHERE room_number IN (101,202,301);

```
