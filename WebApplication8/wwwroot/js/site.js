


let department;

$(document).ready(() => {
    GetVideos();
    GetChennel();
    PrintSideVideos();
    RotingTheScreenOfQuiz()
    PrintSideVideos();
    OnChangeSelectDepartment();
    GetQuiz();
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

function GetBinder(callback) {
    let store;
    $.get('Quiz/GetListOfQuiz', (res) => {
        store = res;
        callback(store)
    }).fail((error) => console.log(error))
}



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
            $('#exampleModal').modal('show');
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



function GetFolderUpdates() {
    $.ajax({
        url: '/ExFile/GetFolders',
        type: 'GET',

        success: function (data) {
            alert("printing the record")

            $('#folderContainer').empty();
            $.each(data, function (key, folder) {

                $('#folderContainer').append('<div >' + folder.name + '</div>');
            });
        },
        error: function (xhr, status, error) {
            console.log("Error occurred: " + error);
        }
    });
}

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
            $('#addFileImage').modal('hide'); // Hide the modal upon error
            alert('There was an error with the file upload');
        }
    });
});


function AddTaskModel() {
    $.ajax({
        url: "/notes/AddNodeModel",
        type: "GET",
        success: function (data) {
            $("#holder").html(data);
            $('#exampleModal').modal('show');
        }
    });
}




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

function GetProfileInfo() {
    let editModelStore = '';
    $.ajax({
        url: '/Account/GetProfileInfo',
        type: 'GET',
        success: function (res) {
            var email = $('#email').val(res.message.email);
            var about = $('#categories').val(res.message.about);
            $('#country-options').val(res.message.countryId);
            $('#state-options').val(res.message.stateId);
            $('#city-options').val(res.message.cityId);
            $('#categories').val(res.message.categories);

            editModelStore += `<div class="modal fade bd-example-modal-lg" id="EditProfile-model" tabindex="-1" role="dialog" aria-labelledby="EditProfileModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
             <div class="modal-content">
             <div class="modal-header">
                <h5 class="modal-title" id="EditProfileModalLabel">Edit Profile</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <div class="form-group">
                    <label for="email">Email</label>
                    <input type="text" id="email" class="form-control" value="${res.message.email}" disabled>
                </div>
                <div class="form-group">
                    <label for="about">About</label>
                    <input id="about" class="form-control"value="${res.message.about}" disabled />
                </div>
                <div class="form-group">
                    <label for="country-options">Country</label>
                    <select id="country-options" class="form-control">
                    <option>
                     value="${res.message.countryId}"
                    </option>
                    </select>
                </div>
                <div class="form-group">
                    <label for="state-options">State</label>
                    <select id="state-options" class="form-control">
                    <option >
                     ${res.message.stateId}
                    </option>
                    </select>
                </div>
                <div class="form-group">
                    <label for="city-options">City</label>
                    <select id="city-options" class="form-control">
                    <option>
                    ${res.message.cityId}
                    </option>
                    </select>
                </div>
                <div class="form-group">
                    <label for="categories">Categories</label>
                    <input type="text" id="categories" class="form-control" placeholder="${res.message.categories}" disabled >
                </div>
                      <div class="form-group">
                    <label for="about">About</label>
                    <input type="text" id="about" class="form-control" placeholder="${res.message.about}" disabled >
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                <button id="saveChangesProfileBtn" type="button" class="btn btn-primary">Save changes</button>
            </div>
        </div>
    </div>
</div>
`
            $("#EditProfile-model").modal('show');


            $("#edit-ProfileModel").html(editModelStore)
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}

function SaveProfileChanges() {
    var updatedProfile = {
        About: $('#about').val(),
        Categories: $('#categories').val(),
        CountryId: $('#country-options').val(),
        CityId: $('#city-options').val(),
        StateId: $('#state-options').val(),
        ProfileImage: $('#e4').val()
    };

    $.ajax({
        url: '/Account/ProfileUpdate',
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(updatedProfile),
        success: function (res) {
            if (res.success) {

                toastr.info('Profile updated successfully');

                alert("");
                $("#EditProfile-model").modal('hide');
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
                    <div class="drive-item module text-center">
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
        alert(selectedIds)
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