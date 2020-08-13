namespace Monitoring.Models
{
    /// <summary>
    /// Интерфейс для ижекции fullset в обход DI для использования мониторинговых атрибутов. Надо наследовать класс, в котором есть помеченный атрибутом метод, от этого интерфейса и инжектить туда fullset в конструкторе
    /// </summary>
    public interface ISetProvider
    {
        StatisticsItemsFullSet StatisticsItemsFullSet { get; }
    }
}
