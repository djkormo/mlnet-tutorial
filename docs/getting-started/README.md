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

<iframe title="YouTube video player" class="youtube-player" type="text/html" width="640" height="385" src="https://www.youtube.com/embed/pYlajl5F6cM?vq=hd1080" frameborder="0" allowFullScreen></iframe>

Start a new web project by executing the following commands

* `mkdir github-issue-classifier`
* `cd github-issue-classifier`
* `dotnet new sln -n GithubIssueClassifier`
* `dotnet new web -n GithubIssueClassifier`
* `dotnet sln add GithubIssueClassifier`

This commands create a new project directory. Then, add a new solution file to the 
project folder. Next, create a new empty web project. And finally, we're adding
the project to the solution file.

After you've executed these commands you can open the solution file in Visual
Studio 2019 or execute `code .` from the project folder to open up Visual Studio
Code.

Now that you have an empty web project, let's add the ML.NET package to it.

## Installing ML.NET in your project

<iframe title="YouTube video player" class="youtube-player" type="text/html" width="640" height="385" src="https://www.youtube.com/embed/5qw4FFBwDCA?vq=hd1080" frameborder="0" allowFullScreen></iframe>

The ML.NET library is available as a nuget package. Execute the following command from the `GithubIssueClassifier` web project folder:

```
dotnet add package Microsoft.ML
```

**Note** Make sure you have a terminal open and navigate 
to `github-issue-classifier/GithubIssueClassifier` before executing 
this command!

## Summary
In this section we've created a new project for the Github issue classifier
and added the ML.NET package to it. In the next section we'll load the dataset
to prepare the model.

[Next section](../loading-data/README.md)

