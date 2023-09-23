using System;

public interface IObservableInputService
{
    event EventHandler InputAppeared;
    event EventHandler InputDisappeared;
}

//public interface IObservableInputService<TInput>
//{
//    event EventHandler<TInput> InputAppeared;
//    event EventHandler<TInput> InputDisappeared;
//    event EventHandler<TInput> InputSet;
//}