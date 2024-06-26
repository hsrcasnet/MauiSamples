﻿using Microsoft.Extensions.Logging;
using PrismMauiApp.Model;
using PrismMauiApp.ViewModels;

namespace PrismMauiApp.Services
{
    public class TodoRepositoryMock : ITodoRepository
    {
        private readonly ILogger<TodoRepositoryMock> logger;
        private readonly ICollection<Todo> todos;

        public TodoRepositoryMock(
            ILogger<TodoRepositoryMock> logger,
            IDateTime dateTime)
        {
            this.logger = logger;

            var now = dateTime.Now;

            this.todos =
            [
                new Todo
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Anmeldung für .NET MAUI Kurs",
                    Done = true,
                    DueDate = new DateTime(2000, 1, 2, 9, 10, 50, DateTimeKind.Local),
                },
                new Todo
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "eBook zu .NET MAUI Entwicklung herunterladen",
                    Link = "https://dotnet.microsoft.com/en-us/download/e-book/maui/pdf",
                    DueDate = new DateTime(2001, 2, 7, 9, 10, 50, DateTimeKind.Local),
                },
                new Todo
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Online-Tutorial zu .NET MAUI auf Youtube schauen",
                    Description = "Willkommen bei der Reihe „.NET MAUI für Anfänger“, in der Sie die Grundlagen zum Erstellen plattformübergreifender Apps mit .NET MAUI für iOS, Android, macOS und Windows anhand einer gemeinsamen C#-Codebasis erlernen. In diesem Video erklärt James, was genau .NET MAUI ist, wie es funktioniert und welche Funktionen Sie kennen müssen, wenn Sie Apps mit dem Framework erstellen.",
                    Link = "https://www.youtube.com/watch?v=Hh279ES_FNQ&list=PLdo4fOcmZ0oUBAdL2NwBpDs32zwGqb9DY",
                    DueDate = now.Date.AddDays(-3),
                },
                new Todo
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "PrismMauiApp mit SQLite erweitern",
                    Description = "Hier gibt's weitere Informationen zum Thema SQLite in .NET MAUI Apps.",
                    Link = "https://github.com/dotnet/maui-samples/tree/main/8.0/Data/TodoSQLite/TodoSQLite",
                    DueDate = now.Date.AddDays(7),
                },
                new Todo
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Kursfeedback ausfüllen",
                    Description = "Nach dem Kurs das Feedback zum Kurs ausfüllen und Verbesserungsvorschläge geben.",
                    DueDate = now.Date.AddDays(14).AddHours(12),
                },
                new Todo
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Ideen für eigene .NET MAUI App",
                    Description = "Diese Notizen unterstützen dich auf der Suche nach einer inspirierenden Idee für deine App-Entwicklung. Hier sind einige Überlegungen:\r\n\r\n" +
                    "- Produktivitätsmanager: Entwickle eine App, die Aufgabenverwaltung, Kalender und Notizen in einer plattformübergreifenden Umgebung integriert.\r\n\r\n" +
                    "- Gesundheits- und Fitnessassistent: Baue eine App, die Fitness-Tracking, Ernährungstagebuch und Gesundheitsberatung bietet.\r\n\r\n" +
                    "- Lokale Entdeckungen: Erstelle eine App, die Nutzern hilft, lokale Veranstaltungen, Restaurants und Sehenswürdigkeiten zu entdecken und zu bewerten.\r\n\r\n" +
                    "- Bildung für Kinder: Konzipiere eine pädagogische App für Kinder, die spielerisch Lernen und kreative Aktivitäten fördert.\r\n\r\n" +
                    "- Personalisierte Reiseplanung: Entwickle eine App, die personalisierte Reiseempfehlungen basierend auf Nutzerpräferenzen und Budget bietet.",
                    DueDate = now.Date.AddMonths(1).AddHours(12),
                },
            ];
        }

        public async Task<bool> AddAsync(Todo item)
        {
            this.logger.LogDebug($"AddAsync: Name={item.Name}");

            this.todos.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(Todo item)
        {
            this.logger.LogDebug($"UpdateAsync: Id={item.Id}");

            var oldItem = this.todos.FirstOrDefault(arg => arg.Id == item.Id);
            this.todos.Remove(oldItem);
            this.todos.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            this.logger.LogDebug($"DeleteAsync: id={id}");

            var oldItem = this.todos.FirstOrDefault(arg => arg.Id == id);
            this.todos.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Todo> GetById(string id)
        {
            this.logger.LogDebug($"GetById: id={id}");

            return await Task.FromResult(this.todos.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Todo>> GetAsync(bool forceRefresh = false)
        {
            this.logger.LogDebug($"GetAsync: forceRefresh={forceRefresh}");

            return await Task.FromResult(this.todos);
        }
    }
}