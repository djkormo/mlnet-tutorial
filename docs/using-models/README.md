# Using models

In the previous section, we've tested the model that we trained. In this section
we'll take a look at how to use ML.NET models in ASP.NET Core.

We'll cover the following topics:

* Preparing the solution
* Building prediction logic
* Making a prediction

Let's get started.

## Preparing the solution

To make a prediction, we'll need a page in the web application that allows the
user to enter some test data. Since this is no an ASP.NET tutorial, we've gone
ahead and prebuilt the page for you.

Copy the contents from the [src](../../src/starter/Website) folder into the
Website project. Make sure to build and run the project to make sure that the
code works as expected.

You can run the website by using the following command from the `Website`
folder:

``` shell
dotnet run
```

When the website works, let's move on to loading the model that we trained in
the previous section.

## Building prediction logic

Before we can make a prediction, we need to load up the model. For this, we're
going to implement a C# class that serves as the wrapper around the model.

We're building the wrapper in three steps:

1. First, we'll create the skeleton of the class
2. Next, we'll load up the model
3. Finally, we'll add the prediction method

Let's get started.

### Creating the predictor skeleton

Add new a new file to the `Model` project called `IssueLabeler.cs` and 
add the following code to the file:

``` csharp
using Microsoft.ML;

namespace GithubIssueClassifier.Model
{
    public class GithubIssueLabeler
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _trainedModel;
        private readonly PredictionEngine<GithubIssue, GithubIssuePrediction> _predictionEngine;

        public IssueLabeler(string modelFile)
        {
            _mlContext = new MLContext();

            //TODO: Initialize model.
        }

        //TODO: Add prediction logic
    }
}
```

The code performs the following steps:

1. First, we define a `GithubIssueLabeler` class that has three fields:
   `_mlContext` for the ML.NET context, `_trainedModel` to store the loaded
   model, and `_predictionEngine` to store the prediction engine instance.
2. Next, we define a new constructor for the `IssueLabeler` class that takes
   the path to the model file.

### Loading the model from disk

When you've created the new `GithubIssueLabeler` class, let's write the code
to load the actual model file.

Copy the following lines to the constructor:

``` csharp
_mlContext = new MLContext();
_trainedModel = _mlContext.Model.Load(modelFile, out var inputSchema);
_predictionEngine = _mlContext.Model.CreatePredictionEngine<GithubIssue, GithubIssuePrediction>(_trainedModel);
```

This code performs the following steps:

1. First, it creates the ML.NET context.
2. Next, it loads the model from disk.
3. Finally, it creates a new prediction engine for the model.

Once we've got the logic to load the model, we can move on to the final step:
Writing the method to make a prediction.

### Writing prediction logic

Add the following code to the `GithubIssueLabeler` class:

``` csharp
public GithubIssuePrediction PredictLabel(GithubIssue issue)
{
    var prediction = _predictionEngine.Predict(issue);
    return prediction;
}
```

This code performs the following steps:

1. First, we define a new method `Predict` that accepts an issue and returns a
   prediction.
2. Next, we call the prediction engine with the issue that was passed to the
   method.
3. Finally, we return the predicted label.

That's all it takes to build the issue labeler. Now we can add it to the website
as a service that we can call from the homepage of the website.

For this we need to make the labeler available as a dependency in ASP.NET Core.
Add the following code to the `ConfigureServices` method in `Startup.cs` of the
`Website` project:

``` csharp
services.AddSingleton<GithubIssueLabeler>(provider =>
{
    var hostingEnvironment = provider.GetRequiredService<IHostingEnvironment>();
    var modelPath = Path.Join(hostingEnvironment.WebRootPath, "GithubClassifier.zip");

    return new GithubIssueLabeler(modelPath);
});
```

This code performs the following steps:

1. First, we add a new singleton service for the `GithubIssueLabeler` class with
   a lambda to specify how the runtime should construct an instance of this
   class.
2. Next, in the lambda, we ask for the hosting environment.
3. After that, we get the webroot path of the hosting environment and combine it
   with the filename of the trained model.
4. Finally, we feed the filename to the constructor of the `GithubIssueLabeler`
   and return the result to the caller.

Now that we have the labeler registered in the application, let's build the code
to predict the homepage.

## Making a prediction
In this final step of the tutorial, we're going to wire up the 
`GithubIssueLabeler` to the page model of the homepage.

Open up the `Index.cshtml.cs` file in the `Pages` folder of the `Website`
project. Add the following code to the top of the class:

``` csharp
private readonly GithubIssueLabeler _githubIssueLabeler;

public IndexModel(GithubIssueLabeler githubIssueLabeler)
{
    _githubIssueLabeler = githubIssueLabeler;
}
```

This code performs the following steps:

1. First, we define a field to store the instance of the `GithubIssueLabeler`
   component.
1. Next, we define a new constructor that accepts a GithubIssueLabeler instance.
2. Finally, we assign the GithubIssueLabeler instance to a private field.

The next step is to wire up the prediction method of the labeler to the page.
Add the following code to the `OnPost` method of the `IndexModel` class:

``` csharp
var issue = new GithubIssue
{
    Title = Input.Title,
    Description = Input.Description
};

var prediction = _githubIssueLabeler.PredictLabel(issue);
PredictedArea = prediction.Area;

return Page();
```

This code performs the following steps:

1. First, we create a new issue from the input that we received from the user.
2. Next, we call the labeler to predict a label for the issue
3. Finally, we assign the result to the `PredictedArea` property.

Once you've got the code in place, run the website from the `Website` folder
using the following command:

```
dotnet run
```

Open up your browser to `https://localhost:5001/` and try it out!

## Summary

In this tutorial, we've explored how to use ML.NET for a multi-class
classification problem. You've learned how to load data, train a model, test it,
and finally use it in your application.

Thank you for your time and hope you enjoyed it!

Feel free to post an issue on this repository if you've found something wrong in
the text. Otherwise, have a great day!