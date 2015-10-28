(function () {
    "use strict";
    var medicationAvg = document.getElementById("medicationAvg").value;
    var symptomAvg = document.getElementById("symptomAvg").value;
    var usersNumber = document.getElementById("usersNumber").value;
    var datesNumber = document.getElementById("datesNumber").value;
    var symptomAndMedAvg = {
        labels: ["Medication Avg", "Symptom Avg"],
        datasets: [
			{
			    fillColor: "rgba(151,187,205,0.5)",
			    strokeColor: "rgba(151,187,205,0.8)",
			    highlightFill: "rgba(151,187,205,0.75)",
			    highlightStroke: "rgba(151,187,205,1)",
			    data: [medicationAvg, symptomAvg]
			}
        ]

    };
    var totalUsers = {
        labels: ["Users",],
        datasets: [
			{
			    fillColor: "rgba(151,187,205,0.5)",
			    strokeColor: "rgba(151,187,205,0.8)",
			    highlightFill: "rgba(151,187,205,0.75)",
			    highlightStroke: "rgba(151,187,205,1)",
			    data: [usersNumber]
			}
        ]

    };
    var totalDays = {
        labels: ["Days"],
        datasets: [
			{
			    fillColor: "rgba(151,187,205,0.5)",
			    strokeColor: "rgba(151,187,205,0.8)",
			    highlightFill: "rgba(151,187,205,0.75)",
			    highlightStroke: "rgba(151,187,205,1)",
			    data: [datesNumber]
			}
        ]

    };
        var ctx = document.getElementById("symptomAndMedAvg").getContext("2d");
        window.myBar = new Chart(ctx).Bar(symptomAndMedAvg, {
            responsive: true
        });
        var ctx = document.getElementById("daysTotal").getContext("2d");
        window.myBar = new Chart(ctx).Bar(totalDays, {
            responsive: true
        });
        var ctx = document.getElementById("usersTotal").getContext("2d");
        window.myBar = new Chart(ctx).Bar(totalUsers, {
            responsive: true
        });
        
}());