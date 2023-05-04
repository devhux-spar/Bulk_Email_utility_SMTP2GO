
var myInterval

function StartTimer() {
    myInterval = setInterval(function () { CallForMsg("myLabel") }, 1000);
}

function StopTimer() {
    clearInterval(myInterval);
}


function MessageUpdate() {

    CallForMsg("myLabel");

}

function CallForMsg(oObj) {
    
$.get('messages', {},
    function (data, status) {
        if (data[0].message != null) {
            document.getElementById(oObj).innerHTML = data[0].message
            if (data[0].count = data[0].totalCount) {
                StopTimer();
            }
        } else {
            document.getElementById(oObj).innerHTML = "";
        }
    });

}
