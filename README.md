# ВЫПОЛНЕНО НА 31.12.2024

- Сохранение данных об игроках в **Postgres 17** с помощью **Entity Framework** и миграций.
- Для обработки JSON использовал **Newtonsoft.JSON** (скоро перейду на **System.Text.Json**).
- Получение данных с API по URL-адресу с помощью **HttpClientFactory**. Создан отдельный класс **ApiClient** для получения ответа от сервера.
- Созданы **3 модели (сущности)**:  
  - **Player** (UserId, Nickname, ClanId, ClanName, Rank, Experience) - таблица **Players**;  
  - **PlayerStats** (UserId, PvpKills, PvpDeath, PvpKd, PveKills, PveDeath, PveKd, LastChecked) - таблица **PlayersStats**;  
  - **PlayerStatsHistory** (UserId, PvpKills, PvpDeath, PvpKd, PveKills, PveDeath, PveKd, Timestamp) - таблица **PlayersStatsHistories**.  
- Создан сервис (**PlayerDataService**) для получения основной информации и контроллер (**PlayerDataController**) к нему.
- Созданы два сервиса:  
  - **TrackerDataService**: получает данные статистики об игроке и сохраняет или обновляет данные в таблицах.  
  - **PlayerStatsUpdater**: раз в час обновляет данные для всех никнеймов игроков из таблицы **PlayersStats**.

---

# ПЛАНЫ ПО РАЗВИТИЮ ПРОЕКТА
- Исправить баг с отображением времени в таблице **PlayersStatsHistories** при автоматическом обновлении.
- Еще что-то...
