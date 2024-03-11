$(document).ready(() => {
    GetVideos();
    GetChennel();
    PrintSideVideos();
});


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
                                <a href="GetSingleVideo/${elem.id}"><img class="img-fluid" src="data:${elem.imageType};base64,${elem.imageData}" alt="Video Image"></a>
                                <div class="time">${elem.videoTitle}</div>
                                </div>
                            <div class="video-card-body">
                                <div class="video-title">
                                    <a href="GetSingleVideo/${elem.id}">${elem.category}</a>
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
    })
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
PrintSideVideos();



function formatDate(dateString) {
    const options = { year: 'numeric', month: '2-digit', day: '2-digit' };
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', options);
}

/* success: function (res) {
     console.log(res);
     $.each(res, (idx, val) => {
        
     })
     $("#Sidebar-videoHolder").html(obj)
 }
})
}

*/



/*
function PrintCourse() {
    let obj = '';
    $.ajax({
        type: 'GET',
        url: '/Message/Result',
        dataType: 'json',
        success: function (res) {
            response = res;
            $.each(res, (idx, val) => {
                obj += ` 
                    <div class="card ">
                        <div class="card-body  ">
                            <h4 class="card-title">${val.name}</h4>
                            <div class="card-text" id="text-container">
                                <h5>Email: ${val.email}</h5>
                                <p>Message: ${val.corders.date}</p>
                            </div>
                            <a href="#" class="btn btn-primary">More</a>
                        </div>
                    </div>`;
            });
            $("#conatner-holder").html(obj);
        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
        }
    });
}


function BackEnd() {
    let store = '';
    $.get('/Message/Result', (res) => {
        $.each(res, (idx, val) => {
            store += `<div class="card m-3 " onmouseover="PopModel(${val.corders.id})">
                        <div class="card-body  ">
                           <h4 class="card-title" >${val.name}</h4>
                           <h4 class="card-title">${val.textMessage}</h4>
                           <div class="card-text" id="text-container">
                           <h5>Email: ${val.corders.email}</h5>
                            <p>Message: ${val.corders.password}</p>
                            </div>
                            <a href="#" class="btn btn-primary">More</a>
                        </div>
                    </div>`;
        });
        $("#conatner-holder").html(store);
    });
}

function DataScience() {
    $.get("Message/CourseResults", (res) => {
        let store = '';
        $.each(res, (idx, val) => {
            store += `<h1 id="${idx}" onmouseover="PopModel(${val.corders.id})">${val.email}</h1>`;
        });
        $("#conatner-holder").html(store);
    });
}

function LifeStyle() {
    $.get("Message/CourseResults", (res) => {
        let store = '';
        $.each(res, (idx, val) => {
            store += `<h1 id="${idx}" onmouseover="PopModel(${val.id})">${val.name}</h1>`;
        });
        $("#conatner-holder").html(store);
    });
}

function PopModel(id) {

    $("#store-popup").html('');


    let store = response.find(x => x.corders.id == id);
    console.log(store)

    $("#store-popup").append(`
    <div class="modal" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">${store.name}</h5>
                        <button type="button" class="close" data-bs-dismiss="modal" aria-lable="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <p>ID: ${store.corders.id}</p>
                        <p>Email: ${store.corders.name}</p>
                        <p>Message: ${store.corders.email}</p>
                       <a href="/${store.corders.id}">Takeme</a>

                    </div>
                    <div class="modal-footer">
                     <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>

                    </div>
                </div>
            </div>
        </div>`
    );
    $('.modal').modal('show');
}
*/