using System;

public interface IObservableInputService
{
    event EventHandler InputAppeared;
    event EventHandler InputDisappeared;
}