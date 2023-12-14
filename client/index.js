
const carsUrl = "http://localhost:5236/api/cars";
let myCars;

async function handleOnLoad() {
    let response = await fetch(carsUrl);
    let data = await response.json();
    myCars = data;
    console.log(data);

    let html = `
        <div class="p-5 mb-4 bg-body-tertiary rounded-3">
            <div class="container-fluid py-5">
                <h1 class="display-5 fw-bold">Julio Jones Used Cars</h1>
                <p class="col-md-8 fs-4">Explore our collection of quality used cars!</p>
            </div>
        </div>

        <div id="tableBody"></div>

        <form onsubmit="return false" class="mt-4">
            <div class="mb-3">
                <label for="make" class="form-label">Make:</label>
                <input type="text" id="make" name="make" class="form-control">
            </div>

            <div class="mb-3">
                <label for="model" class="form-label">Model:</label>
                <input type="text" id="model" name="model" class="form-control">
            </div>

            <div class="mb-3">
                <label for="mileage" class="form-label">Mileage:</label>
                <input type="text" id="mileage" name="mileage" class="form-control">
            </div>

            <div class="mb-3">
            <label for="enterdate" class="form-label">Date:</label>
            <input type="text" id="enterdate" name="enterdate" class="form-control">
        </div>

         
            <button onclick="handleCarAdd()" class="btn btn-primary">Submit</button>
        </form>`;

    document.getElementById('app').innerHTML = html;
    populateCarTable();
}

function populateCarTable() {
    console.log(myCars);
    let html = `
        <table class="table table-striped">
            <tr>
                <th scope="col">Make</th>
                <th scope="col">Model</th>
                <th scope="col">Mileage</th>
                <th scope="col">Enter Date</th>
                <th scope="col">Hold</th>
                <th scope="col">Delete/Sell</th>
            </tr>`;

           myCars.sort((a, b) => new Date(b.enterdate) - new Date(a.enterdate));

    myCars.forEach(function (car) {
        if (!car.deleted) {
            let holdVar = ""

            if(car.isHold == true) {
                holdVar = '❤️'
            }else{holdVar = '♡'}

            html += `
                <tr>
                    <td>${car.make}</td>
                    <td>${car.model}</td>
                    <td>${car.mileage}</td>
                    <td>${car.enterdate}</td>
                    <td><button type="button" class="btn active" data-bs-toggle="button" onclick="handleCarHold(${car.id}, ${car.isHold})">${holdVar}</button></td>
                    <td><button class="btn btn-danger" onclick="handleCarDelete('${car.id}')">Delete/Sell</button></td>
                </tr>`;
        }
    });

    html += `</table>`;
    document.getElementById('tableBody').innerHTML = html;
}

async function handleCarAdd() {

    let car = {
        make: document.getElementById('make').value,
        model: document.getElementById('model').value,
        mileage: document.getElementById('mileage').value,
        enterdate: document.getElementById('enterdate').value,
        isHold: false,
        deleted: false
    };

    await fetch(carsUrl, {
        method: "POST",
        body: JSON.stringify(car),
        headers: {
            "Content-type": "application/json; charset=UTF-8"
        }
    });

    document.getElementById('make').value = '';
    document.getElementById('model').value = '';
    document.getElementById('mileage').value = '';
    document.getElementById('enterdate').value = '';

    await handleOnLoad();
    populateCarTable();
}

async function handleCarDelete(index) 
{
    await fetch(carsUrl + "/" + index, {
        method: "DELETE",
        headers: {
            "Content-type": "application/json; charset=UTF-8"
        }
    });


   await handleOnLoad();
   populateCarTable();
}


async function handleCarHold(index, isHold)
{

    await fetch(carsUrl + "/"+index, {
        method:"PUT",
        body: JSON.stringify(isHold),
        headers: {
            "Content-type": "application/json; charset=UTF-8"
        }
    })
    await handleOnLoad();
    populateCarTable();
}
