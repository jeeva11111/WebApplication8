
let department;

$(document).ready(() => {

    GetVideos();
    GetChennel();
    PrintSideVideos();
    RotingTheScreenOfQuiz()
    PrintSideVideos();
    OnChangeSelectDepartment();
    GetQuiz();
    //  startQuiz()
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