# ORM IN C# and Data Annotations

## Data Annotations

Think about it as the decorators but they are for the properties in a class. (like python’s decorator)

1. Where to put -> above the property declaration.

2. Why we have this? -> Essentially, we will save the amount of code that must be written to handle data mgmt. scenarios.
    Set constraints and limits for the property.

    Input data validation from the frontend. (save so much codes on both ends) Property display and formatting in views.

    Display error messages for the users.

But we can not replace the validation regarding the business logic of course.

You can have one or more data annotation for a single property.

There are data annotations for design models and view models separately. We should not mix these two types of.

Lib path #include System.ComponentModel.DataAnnotations
