$(function () {
    /*$("#button1").click(function () {
        const inputs1 = document.getElementById("input1");
        const inputs2 = document.getElementById("input2");
        const uid = inputs1.value;
        const pw = inputs2.value;
        validation(uid);
        validation(pw);
        inputs1.style.border = "solid 1px black";
        inputs2.style.border = "solid 1px black";
        inputs.value = "";
        $.post('Home/login', { intvalue1: uid, intvalue2: pw }, function (output) {
            console.log(output);
        })
    });*/
})
function validation(item1) {
        if (item1.trim() == "") {
            var userinput = document.getElementById("input1");
            userinput.style.border = "solid 2px red";
            submit1();
        }
}
//deposit
$(function () {
    /*$("#depositbutton").click(function () {
        console.log("hello12");
        const depamount = document.getElementById("depositamount");
        console.log(depamount);
        $.post('Home/deposit', {  depositamount: depamount }, function (output) {
            console.log(output);
        })
    });*/
})
//Withdraw
$(function () {
    /*$("#withdrawbutton").click(function () {
        const withamount = document.getElementById("withdrawamount");
        $.post('Home/withdraw', { userid: uid, withdrawamount: withamount }, function (output) {
            console.log(output);
        })
    });*/
})
//Transfer 
$(function () {
    /* 
    $("#transferbutton").click(function () {
        const amounts = document.getElementById("tamount");
        const accountnos = document.getElementById("taccountno");
        $.post('Home/transfer', { amount:amounts , userid: uid ,accountno: accountnos}, function (output) {
            console.log(output);
        })
    });
    */
})
//CheckBalance
$(function () {
    
    $("#checkbal").click(function () {
        $.post('Home/checkBalance', {  }, function (output) {
            console.log(output);
        })
    });
    
})
//Transaction
$(function () {
    /*
    $("#but05").click(function () {
        $.post('Home/transaction', { userid: uid, }, function (output) {
            console.log(output);
        })
    });
    */
})
//changePin
$(function () {
    /*
    $("#changebutton").click(function () {
        const newpin1 = document.getElementById("pin");
        $.post('Home/changePin', { userid: uid, newpin: newpin1 }, function (output) {
            console.log(output);
        })
    });
    */
})

$(function () {
    /*
    $("#button02").click(function () {
        const inputs1 = document.getElementById("depositamount");
        const amount1 = inputs1.value;
        validation(amount1);
        inputs1.style.border = "solid 1px black";
        document.getElementById("result1").innerHTML= " your deposited"+ amount1 +"  sucessfully "; 
        console.log("your deposited"+ amount1 +"  sucessfully ") ;
    });
    */
})