# LoadModule
 Test task: В разработке находится десктопная игра с относительно частым выпуском публичных апдейтов (месяц и чаще). Каждый апдейт несёт в себе новые механики, сюжетный контент, рефакторинг существующего кода (включая структурные изменения классов) и ассетов, балансные правки, новые интерфейсы и так далее.
Необходимо обеспечить архитектуру, при которой сейв-файлы пользователей совместимы между версиями игры.

Задача со звёздочкой. Части контента вышеупомянутой игры доступны не всем пользователям (т.е. по сути DLC), подключаются как отдельные пакеты (файл или набор файлов со всем контентом этого DLC), добавляют персонажей и события (сцены, диалоги, квесты, предметы) в основной игровой процесс.

[How it works]: Основной скрипт (TestController) весит на камере в сцене, необходимо сделать два запуска сцены, один с _isFirstLaunch = true, второй = false.
Ps. Паттерны: Factory, Strategy, Bridge.
