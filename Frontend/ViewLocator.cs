using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Frontend.ViewModels;
using ReactiveUI;

namespace Frontend; 

public class ViewLocator : IDataTemplate {
    // Check if the provided object is a ViewModel
    public bool Match(object data) => data is ViewModelBase or ReactiveObject;

    // If it is, try to find a matching View
    public IControl Build(object data) {
        // get the type name of the provided object and replace "ViewModel" with "View"
        var name = data.GetType().FullName!.Replace("ViewModel", "View");

        // if the type exists, create an instance of it
        if (Type.GetType(name) is { } type)
            return (Control) Activator.CreateInstance(type)!;
        // otherwise, return a placeholder
        return new TextBlock {Text = "Not Found: " + name};
    }

}