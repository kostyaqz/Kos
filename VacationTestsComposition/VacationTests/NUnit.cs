using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.All)] // Запуск в параллель
[assembly: LevelOfParallelism(3)] // Уровень потоков