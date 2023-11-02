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

Data annotations will restrict the values goes inside the form. But it won’t cover all the special cases based on your business logics. So validation logic is still needed, and you should never rely on the data annotations.

## Data Annotations - Design Model Classes

### Required

This will add not null constraint onto the DB table.

Can use for data and for relations.

Int or Double should NOT use this because they always have a non-null value (unless you have int?)

This is only for string and object.

### StringLength(n)

IF n is not specified, it will be varchar (max). So this will set the max size of the string.

### Key

This is setting the property as the PK on that table inside DB.

Recap, the other two ways are name the property as id or tableNameId.

### Of course there are more, but the most common ones are listed as above


## Data Annotations - View Models Validations

This is used while frontend is taking information from the users.

[Required]:
	The property cannot be null. User cannot skip this field in the form. 
[StringLength(n)] or [StringLength(n,MinimumLength = m)]
	Specify the maximum and minimum length of a string.
[Range(min,max)]
	Ensure a numeric data type is between these two values. 
[Compare(“PropertyName”)] 
	This is used while registering new customers accounts, when they key in passwords twice, this can check the both times matched. 
[ RegularExpression(“regex”)] 
