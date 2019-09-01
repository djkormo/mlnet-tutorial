# Getting started with ML.NET

This section of the tutorial covers how to get started with ML.NET. We'll cover
the following topics:

* What we'll be working on during this tutorial
* Creating a new project
* Installing ML.NET in your project

## What we'll be working on during this tutorial

This tutorial is focused primarily on learning how to use ML.NET in your project.
We will work on a multi-class classifier for labeling Github issues.

In this section of the tutorial we will start by creating a new ASP.NET Core
web application and installing the ML.NET package. 

Let's start by creating an empty web project for the project.

## Creating a new project

[![](http://img.youtube.com/vi/NWQF11Fc9_s/0.jpg)](http://www.youtube.com/watch?v=NWQF11Fc9_s "Creating a new project")

Start a new web project by executing the following commands

* `mkdir github-issue-classifier`
* `cd github-issue-classifier`
* `dotnet new sln -n GithubIssueClassifier`
* `dotnet new web -n Website`
* `dotnet new classlib -n Model`
* `dotnet new console -n Trainer`
* `dotnet sln add Website`
* `dotnet sln add Model`
* `dotnet sln add Trainer`

This commands create a new project directory. Then, add a new solution file to the 
project folder. Next, create a new empty web project. And finally, we're adding
the project to the solution file.

After you've executed these commands you can open the solution file in Visual
Studio 2019 or execute `code .` from the project folder to open up Visual Studio
Code.

Now that you have an empty web project, let's add the ML.NET package to it.

## Installing ML.NET in your project

[![](http://img.youtube.com/vi/ufPhvsKEYAM/0.jpg)](http://www.youtube.com/watch?v=ufPhvsKEYAM "Adding a reference to the ML.NET package")

Execute the following command in each of the project directories that you 
just created.

```
dotnet add package Microsoft.ML
```

This adds a reference to the Microsoft.ML package.

After adding the reference to the ML.NET package, let's finalize the project
layout.

## Finalizing the project layout

[![](http://img.youtube.com/vi/6WZi6UIgwfE/0.jpg)](http://www.youtube.com/watch?v=6WZi6UIgwfE "Adding references the projects")

We now have a solution that has three projects:

* Website - The client that is going to use the model
* Trainer - The console application that is going to train the model
* Model - The components related to the model itself

We haven't setup any references between the projects. We'll do that next.

First, there's the model project that will contain the classes that define 
the input, and output of the model and any other code that relates to the model.

The website project will provide a user interface to use the model. It needs
a reference to the model project in order to use it.

Finally, we have a trainer project that is used to train the machine learning
model. This also needs a reference to the model project.

To set up the references, use the following commands:

* `cd Website`
* `dotnet add reference ../Model`
* `cd ../Trainer`
* `dotnet add reference ../Model`

And that's it, we're good to continue with the next step.

## Summary

In this section we've created a new project for the Github issue classifier
and added the ML.NET package to it. In the next section we'll load the dataset
to prepare the model.

[Next section](../loading-data/README.md)
