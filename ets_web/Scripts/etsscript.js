$(document).ready(function () {
    $(".button").click(function () {
        var isEmply = true;
        $(".manadatory-textbox").each(function () {
            if (this.value == "") {
                isEmply = false;                
            }            
        });
        if(!isEmply)
        {
            alert("Enter all manadatory fields.... ");
            return false;
        }       
    });

    //for reset password validation
    $(".resetbutton").click(function () {

        if ($('.txtmail').val() == "") {
            alert("Enter Mandatory Filed");
            return false;
        }
        else
            return true;
    });

});

 function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to  inactive it?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
   