# Conditional Menu

Different user could have different UI interface and menus. So, you could actually hide certain features from the certain group of users.

You could actually print out the current user authorization information.

For example right now, no account was logged in yet.

![Alt text](image.png)

Once you login:

![Alt text](image-1.png)

Nice but how did we do this?

Manager =>

Emm Request User is a class we created for managing authorization information about the user.

![Alt text](image-2.png)

This is not standard in the code.

Manager -> request User class=>

![Alt text](image-3.png)

![Alt text](image-4.png)

Once the user provide correct credentials to the server, it will validate the credentials on the server and if success, then it will cerate I_Principal object. Then it will encrypted that object pass it to the browser as a cookie.

Every request you make from that point is to keep you signed in. This is a session.

## Optional Claims

_layout template => Nav bar

User account was passed into the layout view.

In the nav bar=>  “is” functions are just for getting the Boolean back.

![Alt text](image-5.png)

Or you could check on the roleClaim to find out if the role is matching the specific type.

![Alt text](image-6.png)

## Manage Accounts(optional)

ASP does not provide any interface for the admin to manage user accounts.

You have to code it yourself.

![Alt text](image-7.png)

## Notes app

You want to have a field to record which user created or updated this object: so in the design model, we already have this field:

![Alt text](image-8.png)

Notice you have to have a RequestUser object in the manager class to access the user did the operation.

![Alt text](image-9.png)

This is user.Name is actually the email address of the user.

![Alt text](image-10.png)

You need to assign the user inside the manager, because you don’t want to ask from the user while on the create page. Because think about that, people can pretend to be someone.

![Alt text](image-11.png)

## Rich Text

How can you create rich text in the front end of the .net mvc

![Alt text](image-12.png)

ACK editor allows you do text editing while you are on the page.

![Alt text](image-13.png)

### Code

Design model is almost same except the content field has a cap on the limit.


ViewModel:

![Alt text](image-14.png)

You set it as a multilineText. Because it is a text field.

![Alt text](image-15.png)

Notice I imported the script over:

![Alt text](image-16.png)

This will allow sck to work 

![Alt text](image-17.png)

This is actually HTML inside the text area.

![Alt text](image-18.png)

ASP.net will not allow HTML sent from the user if you want to bypass the rule

![Alt text](image-19.png)

Change into this

![Alt text](image-20.png)

So this will allow the user to inject codes. In the real world, you must validate the input properly.

# Custom error pages

How to customize the error message so look better than this.

looks like this.

![Alt text](image-21.png)

Production

![Alt text](image-22.png)

Debug while you are in the developer mode

![Alt text](image-23.png)

In the real world, you don’t want to give too much information about the logics behind the scenes. Also, if you give this much of information, it could potentially let the hackers attack your website.

How to do it in the code.

Notice you even have a controller just for the Errors:

![Alt text](image-24.png)

![Alt text](image-25.png)

Server error does have more logics to tell where it is wrong.

Depper look inside the view template in the error page:

![Alt text](image-26.png)

ViewBag 101: This allows you to add any properties you want from the control to the view. 

![Alt text](image-27.png)

View bag is like an JS object so you can add whatever fields into the view bag object. (like a payload)

So, you can get them in the razor template.

![Alt text](image-28.png)

Server error page:

If we are in the debug environment, the detail error message will show.

![Alt text](image-29.png)

This is before we send the request back to the browser-> This is like the filter in nest.js

The coders can write methods to handle events such as:

![Alt text](image-30.png)

MvcApplication is the webapplication is self, inside this class, it has application_endrequest

This request below is every time, before send the response to the client, the logics will be invoked.

Check by the status code and handle error differently:

![Alt text](image-31.png)

![Alt text](image-32.png)

This is based on the error types, we execute the different logics.

## MvcApplication class object:

The object handles one request at a time.

The asp.net runtime will create additional instances of MvcApplication to enable the app to handle multiple requests at the same time (parallel)

How many instances? That number is set by default at the webserver and can be changed by the webserver admin.

While there may be multiple instances of MvcApplication running, the application_Start() method runs only once at the beginning of the app’s lifetime.

So, when you are coding it, please be aware that you could have multiple instances running at the same time.

Use the resources reasonably because you might have 1000 instances running at the same time. Your server might not have enough resources.
