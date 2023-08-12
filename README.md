<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About the project</a>
    </li>
    <li>
      <a href="#built-with">Built with</a>
    </li>
  </ol>
</details>
<br /><br />


## About The Project

This is an MVC application, an online clinic where you can have a "Doctor" role and a "Patient" role.
As an unregistered user you have access to a landing page which contains several menu links with information,
departments, doctors etc.
Data base is seeded with services and departments initially, so you will have to create/register at least one doctor and
one patient to use the full finctionality of the app.
When registering a doctor you might want to give him "Administrator" role manualy as one of the funcionalities requires policy that includes "Doctor"
and "Administrator" roles.
As a docotor who is in "Administrator" role you can create schedule for the available services, check already created schedules for those services.
You can create your own schedule with timeslot that can be booked by patients and check already created ones.
You can write prescriptions to patients containing certain medicaments, if medicament you would want to use is not available you can add it.
Checking your appointments with patients is another option you have. If urgency and some of the booking hours are not available anymore can be canceled 
wich will send email to the patient and notify him.
As a patient you have access to your prescriptions. Access to available services and doctors registered in the system.
You can check their availability and make bookings, check the booking you have made.  Cancelation of bookings can be done and it notifies the administration email, repsctfully,
doctors email if the booking is canceled.
<br /><br />


### Built With

<ul>
  <li>.NET Core 6.0</li>
  <li>ASP.NET Core</li>
  <li>Entity Framework Core</li>
  <li>HTML, CSS, Bootstrap(Bootstrap template)</li>
  <li>MS SQL Server</li>
  <li>NUnit</li>
  <li>Moq</li>
  <li>JS(AJAX, Jquery)</li>
</ul>
<br /><br />
