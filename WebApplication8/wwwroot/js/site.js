﻿


let senderMessageId;
let currentMessageSelectedContent;
let department;


$(document).ready(() => {
    GetVideos();
    GetChennel();
    PrintSideVideos();
    // RotingTheScreenOfQuiz()
    PrintSideVideos();
    OnChangeSelectDepartment();
    // GetQuiz();
    GetNotification();
    ProfileCard();
    GetUserVideos();
    GetFileImages();
    // NotifyCurrentUserMessage();
    //NotifyVideoPostMessage();
    //  startQuiz()
    GetNotify();
    GetFolderUpdates();



    GetCountryList();


    toastr.options = {
        'closeButton': true,
        'debug': false,
        'newestOnTop': false,
        'progressBar': false,
        'positionClass': 'toast-top-right',
        'preventDuplicates': false,
        'showDuration': '1000',
        'hideDuration': '1000',
        'timeOut': '5000',
        'extendedTimeOut': '1000',
        'showEasing': 'swing',
        'hideEasing': 'linear',
        'showMethod': 'fadeIn',
        'hideMethod': 'fadeOut',
        'preventDuplicates': true
    }

    $('#success').click(function (event) {
        toastr.info('You clicked Success toast');
    });



    // Change event for state dropdown
    $('#country-options').on('change', function () {
        var stateId = $(this).val();

        $.ajax({
            url: '/Account/CityGet/',
            type: 'GET',
            data: { stateId: stateId },
            success: function (data) {
                // alert(JSON.stringify(data))
                $('#state-options').empty();

                //  debugger;
                $.each(data, (idx, val) => {
                    //debugger;
                    $('#state-options').append($('<option>', { value: val.countryId }).text(val.stateName));
                })
            }
        });
    });

    $('#state-options').on("change", function () {
        const city = $(this).val()
        $.ajax({
            url: "/Account/GetStateList/",
            type: 'GET',
            data: { cityId: city },
            success: function (res) {
                $('#city-options').empty();
                //  $('#state-options').empty();
                $.each(res, (idx, val) => {
                    $('#city-options').append($('<option>', { value: val.id }).text(val.cityName))
                })
            }
        })
    })

})


// >>---------->>> Getting the Vidoes list : Controller - Chennel
function GetVideos() {
    $.ajax({
        url: '/Chennel/GetVideos',
        type: 'GET',
        success: function (res) {
            let obj = '';
            $.each(res, (idx, elem) => {
                obj += `
                    <div class="col-xl-3 col-sm-6 mb-3">
                        <div class="video-card">
                            <div class="video-card-image">
                                <a class="play-icon"><i class="fas fa-play-circle"></i></a>
                                <a href="/Chennel/GetSingleVideo/${elem.id}"><img id="display-image" class="img-fluid" src="data:${elem.imageType};base64,${elem.imageData}" alt="Video Image"></a>
                                <div class="time">${elem.videoTitle}</div>
                                </div>
                            <div class="video-card-body">
                                <div class="video-title">
                                    <a href="/Chennel/GetSingleVideo/${elem.id}">${elem.category}</a>
                                </div>
                                <div class="video-page text-success">
                                    <div class="video-page text-success">
                                        Education <a title="" data-placement="top" data-toggle="tooltip"  data-original-title="Verified"><i class="fas fa-check-circle text-success"></i></a>
                                    </div>
                                    <div class="video-view">
                                        &nbsp;<i class="fas fa-calendar-alt"></i> ${formatDate(elem.createdDate)}
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>`;
            });

            // Append the generated HTML to the designated element
            $("#video-div").html(obj);
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
        }
    });
}

// -- Date Modify




function GetChennel() {
    let obj = '';
    $.ajax({
        url: '/Chennel/GetChennel',
        type: 'GET',
        success: function (res) {
            console.log("............")
            console.log(res);
            console.log("............")
            let obj = '';
            $.each(res, (idx, elem) => {
                obj += `
                    <div class="col-xl-3 col-sm-6 mb-3">
                        <div class="video-card">
                            <div class="video-card-image">
                                <a class="play-icon"><i class="fas fa-play-circle"></i></a>
                                <a href="Chennel/GetSingleChannel/${elem.chennelId}"><img class="img-fluid" src="data:${elem.imageType};base64,${elem.imageData}" alt="Video Image"></a>
                                <div class="time">${elem.chennelDescription}</div>
                                <p>New line</p>
                                </div>
                            <div class="video-card-body">
                                <div class="video-title">
                                    <a href="Chennel/GetSingleChannel/${elem.chennelId}">${elem.chennelDescription}</a>
                                </div>
                                <div class="video-page text-success">
                                    <div class="video-page text-success">
                                        Education <a title="" data-placement="top" data-toggle="tooltip"  data-original-title="Verified"><i class="fas fa-check-circle text-success"></i></a>
                                    </div>
                               
                                </div>
                            </div>
                        </div>
                    </div>`;
            });

            // Append the generated HTML to the designated element
            $("#chennel-div").html(obj);
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
        }
    });
}


//>>>-------->>  Side -video loading >>----------->>>

function PrintSideVideos() {

    let obj = '';
    $.ajax({
        url: '/Chennel/GetVideos',
        type: 'GET',
        success: function (res) {
            let random = res.sort(() => Math.random() - 0.5);

            console.log(res)
            obj += `<h3 class="mb-4">Next Video</h1>`;
            $.each(random, (idx, elem) => {

                if (idx < 8 && elem.imageType !== null) {
                    console.log("Value is printed")
                    obj += `<div class="row">
                        <div class="col-md-12">
                            <div class="video-card video-card-list">
                                <div class="video-card-image">
                                    <a class="play-icon" href="#"><i class="fas fa-play-circle"></i></a>
                                     <a href="Chennel/GetSingleVideo/${elem.id}"><img class="img-fluid" src="data:${elem.imageType};base64,${elem.imageData}" alt="Video Image"></a>
                                    <div class="time">${elem.category}</div>
                                </div>
                                <div class="video-card-body">                    
                                    <div class="btn-group float-right right-action">
                                        <a href="#" class="right-action-link text-gray" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            <i class="fa fa-ellipsis-v" aria-hidden="true"></i>
                                        </a>
                                        <div class="dropdown-menu dropdown-menu-right">
                                            <a class="dropdown-item" href="#"><i class="fas fa-fw fa-star"></i> &nbsp; Top Rated</a>
                                            <a class="dropdown-item" href="#"><i class="fas fa-fw fa-signal"></i> &nbsp; Viewed</a>
                                            <a class="dropdown-item" href="#"><i class="fas fa-fw fa-times-circle"></i> &nbsp; Close</a>
                                        </div>
                                    </div>
                                  
                                       <div class="video-title">
                                      <a href="/Chennel/GetSingleVideo/${elem.id}">${elem.category}</a>
                                    </div>
                                    <div class="video-page text-success">
                                        Education  <a title="" data-placement="top" data-toggle="tooltip" href="#" data-original-title="Verified"><i class="fas fa-check-circle text-success"></i></a>
                                    </div>
                                    <div class="video-view">
                                        1.8M views &nbsp;<i class="fas fa-calendar-alt"></i> ${formatDate(elem.createdDate)}
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>`;
                }
            });
            $("#side-videoloder").html(obj);
        },
    });
}

// Calling the function side videos




function formatDate(dateString) {
    const options = { year: 'numeric', month: '2-digit', day: '2-digit' };
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', options);
}



//----------> Getting the Quiz
// >>>------------->>Getting the Quiz Qustions<<<------------<<


function GetQuiz() {

    $.ajax({
        url: 'Quiz/GetListOfQuiz',
        type: 'GET',
        contentType: 'json',
        success: function (res) {
            department = res;
            quizQuestions = res;
            console.log(quizQuestions)
        },
        error: function (xhr, status, error) {
            console.error('Error occurred while fetching quiz questions: ' + error);
        }
    });
}


// >>>------------->>Getting the Quiz Qustions<<<------------<<


function OnChangeSelectDepartment() {
    $('#SelectDept').change(function () {
        //  alert(this.value)
        let selectedDepartment = $(this).val();
        FindTheMatchingRequest(selectedDepartment);
    });
}


function FindTheMatchingRequest(DepValue) {
    let store = department.quizItems.filter(question => question.department === DepValue);
    BindOptions(store)
}

//function GetBinder(callback) {
//    let store;
//    $.get('Quiz/GetListOfQuiz', (res) => {
//        store = res;
//        callback(store)
//    }).fail((error) => console.log(error))
//}



//function to store the values
function BindOptions(quizData) {
    var quizContainer = $('#quizContainer');
    quizContainer.empty();

    if (quizData.length > 0) {
        $.each(quizData, function (index, quiz) {
            console.log(quiz)
            var questionHtml = `
                    <p>${quiz.question}</p>
                    <div class="options">`;
            questionHtml += `</div></div > `;
            quizContainer.append(questionHtml);

        });
    } else {
        quizContainer.html("<p>No quizzes found for this department.</p>");
    }
}



// >>>------------->> Quiz  Controller<<--------------<<<

function RotingTheScreenOfQuiz() {
    var prev = $("#prevBtn");
    var next = $("#nextBtn");
    let currentIndex = 0;
    let quizData = [];

    prev.on("click", () => {
        if (currentIndex > 0) {
            currentIndex--;
            displayQuestion(currentIndex);
        }
    });

    next.on("click", () => {
        if (currentIndex < quizData.length - 1) {
            currentIndex++;
            displayQuestion(currentIndex);
        }
    });

    // GetQuizData();

    function displayQuestion(index) {
        let quiz = quizData[index];
        var quizContainer = $('#quizContainer');
        quizContainer.empty();

        if (quiz) {
            var questionHtml = `
                <div class="question">
                    <p>${quiz.question}</p>
                    <div class="options">`;

            // Loop through each option and add radio buttons
            $.each(quiz.depOptionsLists, function (optionIndex, option) {
                questionHtml += `
                    <label>
                        <input type="radio" name="question_${index}" value="${option.title}" />
                        ${option.title}
                    </label><br />`;
            });

            questionHtml += `</div></div>`;
            quizContainer.append(questionHtml);
        } else {
            quizContainer.html("<p>No quizzes found.</p>");
        }
    }
}


//function to store the values



function GetDetailsOfCourse(Id) {
    $('.more-info').on('click', function (e) {
        e.preventDefault();
        var url = $(this).attr('href');

        $.ajax({
            url: url,
            type: 'GET',
            success: function (res) {

                alert(Id);
                console.log(res);
                window.location.href = url;

            },
            error: function (xhr, status, error) {

                console.error(xhr.responseText);
                alert('An error occurred while fetching course details.');
            }
        });

    });
}

/*Quiz Model part*/



let currentQuestionIndex = 0;
let score = 0;
let timeLeft = 30;
let timerInterval;
let quizQuestions;

// Function to start the quiz
function startQuiz() {
    $("#start-button").hide();
    displayQuestion();
    startTimer();
}

// Function to display a question and its options
function displayQuestion() {
    const currentQuestion = quizQuestions[currentQuestionIndex];
    const questionText = $("#question-text");
    const answerButtons = $("#answer-buttons");

    // Clear previous question and answer options
    questionText.empty();
    answerButtons.empty();

    // Display the current question
    questionText.text(currentQuestion.question);

    if (currentQuestionIndex < quizQuestions.length) {
        const currentQuestion = quizQuestions[currentQuestionIndex];
        questionText.text(currentQuestion.question);

        currentQuestion.quizItems.forEach(option => {
            const button = $("<button></button>");
            button.text(option);
            button.addClass("answer-button");
            answerButtons.append(button);

            // Add click event listener to check the answer
            button.on("click", function () {
                checkAnswer(option);
            });
        });


    }

    // Function to check the selected answer
    function checkAnswer(selectedOption) {
        const currentQuestion = quizQuestions[currentQuestionIndex];

        // Check if the selected answer is correct
        if (selectedOption === currentQuestion.correctAnswer) {
            score++;
        }

        // Move to the next question or end the quiz if all questions are answered
        currentQuestionIndex++;

        if (currentQuestionIndex < quizQuestions.length) {
            displayQuestion();
        } else {
            endQuiz();
        }
    }

    // Function to start the timer
    function startTimer() {
        timerInterval = setInterval(function () {
            timeLeft--;

            // Update the timer text
            $("#timer").text(timeLeft);

            // End the quiz if time runs out
            if (timeLeft <= 0) {
                endQuiz();
            }
        }, 1000);
    }

    // Function to end the quiz
    function endQuiz() {
        // Stop the timer
        clearInterval(timerInterval);

        // Calculate the score percentage
        const scorePercentage = (score / quizQuestions.length) * 100;

        // Display the final score
        const questionContainer = $("#question-container");
        questionContainer.html(`
      <h2>Quiz Completed!</h2>
      <p>Your Score: ${score} out of ${quizQuestions.length}</p>
      <p>Score Percentage: ${scorePercentage}%</p>
    `);
    }

    // AJAX request to get quiz questions
    $.ajax({
        url: 'Quiz/GetListOfQuiz',
        type: 'GET',
        success: function (res) {
            quizQuestions = res.quizItems; // Assuming quizItems is the array of questions
        },
        error: function (xhr, status, error) {
            console.error('Error occurred while fetching quiz questions: ' + error);
        },
        complete: function () {
            // Call startQuiz once quizQuestions is populated
            startQuiz();
        }
    });

}


// Add Notes models

function AddNodeModel() {


    $.ajax({
        url: "/notes/AddNodeModel",
        type: "GET",
        success: function (data) {
            $("#holder").html(data);
            $('#exampleModalRE').modal('show');
        }
    });
}


function logoutModelCaller() {
    $("#logoutModal").modal("show");
}

function logoutModelHider() {
    $("#logoutModal").modal("hide");
}


// Getting Notification



function GetNotification() {
    var i = 0;
    $.ajax({
        url: '/Account/JsonRetrun',
        type: 'GET',
        success: function (res) {
            console.log(res);
            // Set the modal content
            $("#title").text(res.title);
            $("#message").text(res.message);
            $("#email").text(res.email);
            // Show the modal
            $("#notifaction-container").css("display", "block");
            $("#notificationModal").modal("show");
            $("#notify-msg").text(i += 1);
        },
        error: function (error) {
            console.log(error);
        }
    });
}

function GetNotify() {
    $.ajax({
        url: '/Video/GetAllNotify',
        type: 'GET',
        success: function (res) {
            console.log(res)
            $.each(res, (idx, val) => {
                jQuery('#v-pills-inbox').css("overflow-y", "scroll");
                $("#v-pills-inbox").append(`<i>${val.message}</i>`)
                $("#v-pills-inbox").append(`<hr/>`)
                $("#v-pills-inbox").append(`<i>${val.title}</i>`)
                $("#v-pills-inbox").append(`<hr/>`)

            })
        }
    })
}


function GetServices() {
    $.ajax({
        url: '/BookMarks/JsonArray',
        type: 'GET',
        success: function (res) {

            $("#app-services").css('display', 'block');
        }
    });
}

//function NotifyCurrentUserMessage() {
//    $.ajax({
//        url: 'Account/GetCurrentUser',
//        type: 'GET',
//        success: function (res) {
//            console.log(res);
//        }
//    })
//}





// Profile cards 
function ProfileCard() {
    $.ajax({
        url: '/Account/ProfileCardDetails',
        type: 'GET',
        success: function (res) {
            let userProfileCount = res.result;
            // Create HTML for each card using received data
            let cardsHtml = `
                <div class="row" id="profile-cards-list">
                    <div class="col-xl-3 col-sm-6 mb-3">
                        <div class="card text-white bg-warning o-hidden h-100 border-none">
                            <div class="card-body">
                                <div class="card-body-icon">
                                    <i class="fas fa-fw fa-video"></i>
                                </div>
                                <div class="mr-5"><b>${userProfileCount.subscribers}</b> Subscribers</div>
                            </div>
                            <a class="card-footer text-white clearfix small z-1" href="#">
                                <span class="float-left">View Details</span>
                                <span class="float-right">
                                    <i class="fas fa-angle-right"></i>
                                </span>
                            </a>
                        </div>
                    </div>

                    <div class="col-xl-3 col-sm-6 mb-3">
                        <div class="card text-white bg-success o-hidden h-100 border-none">
                            <div class="card-body">
                                <div class="card-body-icon">
                                    <i class="fas fa-fw fa-list-alt"></i>
                                </div>
                                <div class="mr-5"><b>${userProfileCount.audioCount}</b> Audios Posted</div>
                            </div>
                            <a class="card-footer text-white clearfix small z-1" href="#">
                                <span class="float-left">View Details</span>
                                <span class="float-right">
                                    <i class="fas fa-angle-right"></i>
                                </span>
                            </a>
                        </div>
                    </div>

                    <div class="col-xl-3 col-sm-6 mb-3">
                        <div class="card text-white bg-danger o-hidden h-100 border-none">
                            <div class="card-body">
                                <div class="card-body-icon">
                                    <i class="fas fa-fw fa-cloud-upload-alt"></i>
                                </div>
                                <div class="mr-5"><b>${userProfileCount.videoCount}</b> New Videos</div>
                            </div>
                            <a class="card-footer text-white clearfix small z-1" href="#">
                                <span class="float-left">View Details</span>
                                <span class="float-right">
                                    <i class="fas fa-angle-right"></i>
                                </span>
                            </a>
                        </div>
                    </div>
                </div>
            `;
            $("#holder-card-profile").html(cardsHtml);
        },
        error: function (error) {
            console.log(error);
        }
    });
}


// Induvidual Videos



function GetUserVideos() {
    $.ajax({
        url: '/Account/InduvialVideoPostedList',
        type: 'GET',
        success: function (res) {
            console.log(res);
            $("#profile-video-list").empty();

            if (res.message && res.message.length > 0) {
                $.each(res.message, function (idx, val) {
                    var videoCard = `
                        <div class="col-xl-3 col-sm-6 mb-3">
                            <div class="video-card">
                                <div class="video-card-image">
                                    <a class="play-icon" href="#"><i class="fas fa-play-circle"></i></a>

                                            <a href="Chennel/GetSingleChannel/${val.chennelId}"><img class="img-fluid" src="data:${val.imageType};base64,${val.imageData}" alt="Video Image"></a>
                                <div class="time">${val.chennelDescription}</div>
                                    <!-- Use val.image which should be the correct image path -->
                                    <div class="time">3:50</div>
                                </div>
                                <div class="video-card-body">
                                    <div class="video-title">
                                        <a href="#">${val.title}</a>
                                    </div>
                                    <div class="video-page text-success">
                                        <a title="" data-placement="top" data-toggle="tooltip" href="#" data-original-title="Verified">
                                            <i class="fas fa-check-circle text-success"></i>
                                        </a>
                                    </div>
                                    <div class="video-view">
                                        1.8M views &nbsp;<i class="fas fa-calendar-alt"></i> 11 Months ago
                                    </div>
                                </div>
                            </div>
                        </div>`;
                    $("#profile-video-list").append(videoCard);
                });
            } else {
                $("#profile-video-list").html("<p>No videos found.</p>");
            }
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
        }
    });
}



//function GetFolderUpdates() {
//    $.ajax({
//        url: '/ExFile/GetFolders',
//        type: 'GET',

//        success: function (data) {
//            alert("printing the record")

//            $('#folderContainer').empty();
//            $.each(data, function (key, folder) {

//                $('#folderContainer').append('<div >' + folder.name + '</div>');
//            });
//        },
//        error: function (xhr, status, error) {
//            console.log("Error occurred: " + error);
//        }
//    });
//}

$('#fileUploadForm').on('submit', function (e) {
    e.preventDefault();

    var formData = new FormData(this);

    $.ajax({
        url: '/ExFile/AddFile',
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (data) {
            // debugger;
            GetFileImages();
            $('#addFileImage').modal("hide");
            $("#addFileImage").hide();
        },
        error: function () {
            $('#addFileImage').modal('hide');
            alert('There was an error with the file upload');
        }
    });
});






function GetAllFilesFromUploadsFolder() {
    $.ajax({
        url: '/ExFile/GettingExcelData',
        type: 'GET',
        success: function (res) {
            console.log(res);
            // Correct usage of template literals here.
            // $("#FolderStore").append(`<h1>${res.message}</h1>`);
        },
        error: function (err) {
            console.error("Error fetching data: ", err);
        }
    });
}

("#country-options").on('change', function () {
    alert("----------")
})



function GetCountryList() {
    {
        $.ajax({
            url: '/Account/GetCountry',
            type: 'GET',
            success: function (data) {
                $.each(data, function (i, state) {
                    $('#country-options').append($('<option>', {
                        value: state.id,
                        text: state.name
                    }));
                });
            }
        });
    }
}



function UpdateProfileModel() {
    //let obj = { name: $("#pro-name").val(), email: $("#pro-email").val(), roles: $("#pro-roles") }
    $.ajax({
        url: '/Account/ProfileInfo',
        type: 'GET',

        success: function (res) {
            $("#pro-name").val(res.stateModel.name)
            $("#pro-about").val(res.stateModel.about)
            $("#pro-roles").val(res.stateModel.roles)

            $("#pro-model").modal("show");
        }
    })
}




//-------------------


function UpdateNotesModel(id) {
    $.ajax({
        url: '/Notes/GetNotes/' + id,
        type: 'GET',
        contentType: 'application/json',
        success: function (response) {
            var note = response.message;
            $("#holderC1").html(response);
            $("#task-name").val(note.taskName);
            $("#task-color").val(note.color);
            $("#task-description").val(note.description);
            $("#task-project").val(note.projectTitle);
            $("#task-date").val(note.dateTime);
            $("#task-check").prop('checked', note.starred);
            $("#exampleModalRE").modal('show');
        },
        error: function (xhr, textStatus, errorThrown) {
            console.error('Error updating profile:', textStatus, errorThrown);
        }
    });
}

// add the new values to Notes
$("#notePadSubmitForm").click(function () {
    // debugger;
    //  var s = 0;
});

//$("#notePadSubmitForm").on('click', function (e) {
//    //e.preventDefault();
//    var formData = new FormData(this);
//    $.ajax({
//        url: '/Notes/UploadImage',
//        type: 'POST',
//        contentType: false,
//        processData: false,
//        data: formData,
//        success: function (res) {
//            console.log("Data added successfully");
//            $("#exampleModalRE").modal('hide');
//        },
//        error: function (xhr, textStatus, errorThrown) {
//            console.error('Error adding data:', textStatus, errorThrown);
//        }
//    });
//});






function updateNotesModelInputs() {

    var obj = {
        taskName: $('#task-name').val(),
        color: $('#task-color').val(),
        description: $('#task-description').val(),
        projectTitle: $('#task-project').val(),
        dateTime: $('#task-date').val(),
        starred: $('#task-check').prop('checked')
    };

    $.ajax({
        url: '/Notes/EditProfile',
        type: 'PUT',
        data: JSON.stringify(obj),
        contentType: 'application/json',
        success: function (res) {

            $("#task-name").val('');
            $("#task-color").val('');
            $("#task-description").val('');
            $("#task-project").val('');
            $("#task-date").val('');
            $("#task-check").prop('checked', false);

            $("#exampleModalRE").modal('hide');
        },
        error: function (xhr, textStatus, errorThrown) {
            console.error('Error updating profile:', textStatus, errorThrown);

            alert('Error updating profile: ' + xhr.responseJSON.message);
        }
    });
}


function GetProfileInfo() {
    $.ajax({
        url: '/Account/GetProfileInfo',
        type: 'GET',
        success: function (res) {
            if (res && res.message) {
                $('#pro-name').val(res.message.name);
                $('pro-about').val(res.message.about);
                $('#country-options').val(res.message.countryId);
                $('#state-options').val(res.message.stateId);
                $('#city-options').val(res.message.cityId);
                $('#categories').val(res.message.categories);

                // Show the modal after setting values
                $("#EditProfile-model").modal('show');
            } else {
                alert("Profile information not available.");
            }
        },
        error: function (xhr, status, error) {
            alert("Failed to retrieve profile information: " + error);
        }
    });
}


function SaveProfileChanges() {
    var updatedProfile = {
        name: $('#pro-name').val(),
        about: $('#pro-about').val(),
        //CountryId: $('#country-options').val(),
        //  CityId: $('#city-options').val(),
        // StateId: $('#state-options').val(),
        //  ProfileImage: $('#pro-image').val() // Assuming there's an input field for profile image
    };

    $.ajax({
        url: '/Account/ProfileUpdate',
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(updatedProfile),
        success: function (res) {
            if (res.success) {
                toastr.info('Profile updated successfully');
                $("#pro-model").modal('hide');
            } else {
                toastr.error("Failed to update profile: " + res.message);
            }
        },
        error: function (xhr, status, error) {
            alert("Failed to update profile: " + error);
        }
    });
}

$('#saveChangesProfileBtn').on('click', function () {
    SaveProfileChanges();
});



function RefreshingBlock() {
    GetFileImages();
}


function GetFileImages() {
    let storeListItems = '';
    $.ajax({
        url: '/ExFile/GetFile',
        type: 'GET',
        success: function (res) {
            $.each(res, function (idx, val) {
                const fileIcon = val.fileName.includes("xlsx") ? '<i class="fa fa-file-excel-o text-success"></i>' : '<i class="fa fa-file-text-o text-primary"></i>';
                storeListItems += `
                    <div class="drive-item module text-center " id="Exfile-holder">
                        <div class="drive-item-inner module-inner">
                            <div class="drive-item-title">
                                <div id="FolderStore"></div>
                            </div>
                            <div class="drive-item-thumb">
                                <a href="#">${fileIcon}</a>
                                <p>${val.fileName}</p>
                            </div>
                        </div>
                        <div class="drive-item-footer module-footer">
                            <ul class="utilities list-inline">
                                <li>
                                    <a href="#" data-toggle="tooltip" data-placement="top" title="" data-original-title="Download" onclick="downloadFile(${val.id})">
                                        <i class="fa fa-download"></i>
                                    </a>
                                </li>
                                <li>
                                    <a href="#" data-toggle="tooltip" data-placement="top" title="" onclick="DeleteFile(${val.id})" data-original-title="Delete">
                                        <i class="fa fa-trash"></i>
                                    </a>
                                </li>
                                <li>
                                    <input name="radioName" type="checkbox" value=${val.id} />
                                </li>
                            </ul>
                        </div>
                    </div>`;
            });
            $("#Documents-list-exFile").html(storeListItems);
            $("#exampleModalCenter").hide();
        },
        error: function (xhr, status, error) {
            toastr.error("Unable to display the document list: " + error.message);
        }
    });
}




function collectSelectedFiles() {
    var selectedIds = [];
    $('input[name="radioName"]:checked').each(function () {
        selectedIds.push($(this).val());
    });


    if (selectedIds.length > 0) {
        // alert(selectedIds)
        //toastr.info("No files selected for deletion.")
        DeleteMultipleFiles(selectedIds);
    } else {
        toastr.error("No files selected for deletion.");
    }
}


function DeleteMultipleFiles(ids) {
    ids.forEach(function (id, index) {
        $.ajax({
            url: '/ExFile/deleteFile/' + id,
            type: "POST",
            success: function (res) {
                toastr.info("File with ID " + id + " has been deleted");
            },
            error: function (xhr, status, error) {
                toastr.error("Unable to delete the file with ID " + id + ". Error: " + xhr.responseText);
            },
            complete: function () {
                if (index === ids.length - 1) {
                    GetFileImages();
                }
            }
        });
    });
}



function DeleteFile(id) {
    $.ajax({
        url: '/ExFile/deleteFile/' + id,
        type: "POST",
        success: function (res) {
            toastr.info("Image has been deleted")
            //   location.reload();
            GetFileImages();

        }, error: function (xhr, status, error) {
            toastr.error("Unable to delete the image ");
        }
    })
}


// Download file from serverSide ;




function downloadFile(id) {
    var downloadUrl = '/ExFile/DownloadFile/' + id;

    // Create a hidden anchor element
    var anchor = document.createElement('a');
    anchor.href = downloadUrl;
    anchor.target = '_blank';
    anchor.download = '';


    document.body.appendChild(anchor);
    anchor.click();


    document.body.removeChild(anchor);

    toastr.success("File download initiated.");
}


function FileManagerAddFile() {
    $.ajax({
        url: '/FileManager/addFile',
        type: 'GET',
        success: function (res) {
            console.log(res);
        }, error: function (xhr, status, error) {
            console.log(error);
        }
    })
}



$("#messageForm").on("submit", function (e) {
    e.preventDefault();
    var message = $("#sendSmsInput").val();
    var userId = $("#UserIdInput").val();
    var obj = { message: message, senderId: userId };
    alert(JSON.stringify(obj))
    $.ajax({
        url: '/sendMessage',
        type: 'POST',
        data: obj,
        contentType: 'application/json',
        dataType: 'json',
        success: function (response) {
            console.log('Success:', response);
            alert("Message sent successfully!");
        },
        error: function (xhr, status, error) {
            console.log('Error:', error);
            alert("Failed to send message.");
        }
    });
})



//function postMessage() {
//    //debugger;
//    // e.preventDefault();
//    var message = $("#sendSmsInput").val();
//    var userId = $("#UserIdInput").val();
//    var obj = { message: message, senderId: userId };
//    alert(JSON.stringify(obj))
//    $.ajax({
//        url: '/sendMessage',
//        type: 'POST',
//        data: obj,
//        contentType: 'application/json',
//        dataType: 'json',
//        success: function (response) {
//            console.log('Success:', response);
//            alert("Message sent successfully!");
//        },
//        error: function (xhr, status, error) {
//            console.log('Error:', error);
//            alert("Failed to send message.");
//        }
//    });
//}




function GetAllUserNames() {
    let obj = '';
    $.ajax({
        url: '/Message/GetAllUser',
        type: 'GET',
        contentType: 'application/json',
        success: function (res) {
            var users = res.currentList;
            $.each(users, function (idx, val) {
                obj += `<div class="chat_list">
                            <div class="chat_people">
                                <a  onclick="GetUserInfo(${val.id})">
                                   <img id="display-image" class="img-fluid" src="data:${val.imageType};base64,${val.imageData}" alt="Video Image">
                                </a>
                                <div class="chat_ib">
                                    <h5>${val.name} <span class="chat_date">Dec 25</span></h5>
                                
                                    </div>
                            </div>

                        </div>`;
            });
            $("#chat-list-people").html(obj);
        },
        error: function (error) {
            alert(error);
        }
    });
}




function GetUserInfo(userId) {

    $.ajax({
        url: '/Message/GetUserInfo',
        type: 'GET',
        data: { userId: userId },
        success: function (response) {
            $("#senderName").text(response.name)
            console.log(response);
            GetReciverMessage(response.id);
            GetSenderMessage(response.id);
         
            senderMessageId = response.id;
        },
        error: function (error) {
            alert(error);
        }
    });
}


function sendMessageToUser(userId) {
    var inputValues = $("#message-text");
    var obj = { message: inputValues, senderId: userId };

    $.ajax({
        url: '/Message/SendMessage',
        type: 'POST',
        contentType: 'application/json',
        data: { obj },
        success: function (res) {
            alert(res)
        }, error: function (xhr, status, error) {
            alert(error)
        }
    })
}


$("#exFile-search").on('submit', function (e) {
    e.preventDefault();
    var inputValue = $(this).find('input[type="search"]').val();

    $.ajax({
        url: '/ExFile/ExFileSearch',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(inputValue),
        success: function (res) {
            console.log(res);
            $("#Exfile-holder").html(res.message);
        }
    });
});


function addAssignment() {
    $.ajax({
        type: 'GET',
        url: '/Admin/AddAssignment',
        success: function (res) {
            $("#Assign-model").html(res);
            $("#addAssignment").modal('show');
        }
    })
}


$("#NumberOfPer").on('chnage', function () {
    var numberOfRows = document.getElementById("NumberOfPer").value;

    $("#assign-tbl tbody").find("tr:not(:first)").remove();

    for (var i = 0; i < numberOfRows - 1; i++) {
        var table = document.getElementById("assign-tbl");

        var rows = table.getElementsByTagName("tr");

        var rowsOuterHtml = rows[rows.length - 1].outerHTML;
        var lastrowIdx = document.getElementById('LastIndex').value;

        var nextrowIdx = eval(lastrowIdx) + 1;


    }
})




function TextMessage() {

    var messageText = $("#message-text").val();
    var senderId = senderMessageId;

    $.ajax({
        url: '/Message/whatsApp',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ senderId: senderId, textMessage: messageText }),
        success: function (res) {
            toastr.info(`Message has been sent`);
            res.senderId = currentMessageSelectedContent;

            $("#message-text").val("");
        },
        error: function (xhr, status, error) {
            toastr.error('Unable to send the message');
        }
    });
}


function GetReciverMessage(userId) {

    $.ajax({
        url: '/Message/GetReciverMessage',
        type: 'GET',
        contentType: 'application/json',
        data: { userId: userId },
        success: function (res) {
            var messages = res.message;
            $(".msg_history").empty();
            messages.forEach(function (msg) {
                var message = `<div class="incoming_msg">
                                    <div class="incoming_msg_img">
                                        <img src="https://bootdey.com/img/Content/avatar/avatar1.png" alt="user">
                                    </div>
                                    <div class="received_msg">
                                        <div class="received_withd_msg">
                                            <p>${msg.textMessage}</p>
                                            <span class="time_date">Received | ${new Date(msg.timeStamp).toLocaleString()}</span>
                                        </div>
                                    </div>
                                </div>`;
                $(".msg_history").append(message);
            });
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}


function GetSenderMessage(userId) {

    $.ajax({
        url: '/Message/GetSendMessage',
        type: 'GET',
        data: { userId: userId },
        success: function (res) {
            var reponse = res.message;

            $.each(reponse, function (idx, val) {

                var sentMessage = `<div class="outgoing_msg">
                                    <div class="sent_msg">
                                        <p>${val.textMessage}</p>
                                        <span class="time_date">Sent | ${new Date().toLocaleString()}</span>
                                    </div>
                                </div>`;
                $(".msg_history").append(sentMessage);
                $("#message-text").val("");
            })
        }
    })
}