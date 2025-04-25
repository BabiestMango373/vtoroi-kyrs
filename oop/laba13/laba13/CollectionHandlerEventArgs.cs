using System;
using System.Collections;

delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);

public class CollectionHandlerEventArgs : EventArgs
{
    public string CollectionName { get; set; }

    public string TypeChange { get; set; }

    public object ChangeObj { get; set; }

    public CollectionHandlerEventArgs(string collectionName, string typeChange, object changeObj)
    {
        CollectionName = collectionName;
        TypeChange = typeChange;
        ChangeObj = changeObj;
    }

    public override string ToString()
    {
        return $"Коллекция: {CollectionName}, Тип изменений: {TypeChange}, Объект: {ChangeObj}";
    }
}
