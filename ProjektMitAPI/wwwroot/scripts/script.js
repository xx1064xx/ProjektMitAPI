let account = false; // simuliert an- oder abgemeldeten Nutzer




function toggleProfile() {
    var profilePage = document.getElementById('profilePage');

    const token = localStorage.getItem('jwt-token');
    if (token != null) {
        account = true;
    }
    else {
        account = false;
    }


    
    
    // Wenn das Div sichtbar ist, wird es ausgeblendet

    if (account) {
        console.log('Benutzer angemeldet');
        if (profilePage.style.overflow !== 'visible') {
            profilePage.style.display = 'visible';
            profilePage.style.height = '65vh';
            profilePage.style.opacity = '1';
            profilePage.style.overflow = 'visible';
            setAccInfoText();
        } else {
            // Wenn das Div ausgeblendet ist, wird es wieder eingeblendet
            //profilePage.style.display = 'none';
            profilePage.style.height = '0px';
            profilePage.style.opacity = '0';
            profilePage.style.overflow = 'hidden';

            
        }
    }
    else {
        window.location.href = '/login'; // Leitet zum Login weiter falls man nicht angemeldet ist
    }
}

function toggleMenu() {
    const menu = document.querySelector('.hamburger-menu');
    menu.classList.toggle('active');

    const middleSpan = document.querySelector('.hide-on-click');
    middleSpan.style.opacity = (menu.classList.contains('active')) ? '0' : '1';
    const header = document.querySelector('header');
    const nav = document.querySelector('nav');
    const logo = document.querySelector('img');

    header.classList.toggle('expanded');
    nav.classList.toggle('expanded');
    logo.classList.toggle('expanded');

    const headerList = document.getElementById('headerList');
    if (headerList.style.overflow !== 'visible') {
        headerList.style.height = '300px';
        headerList.style.opacity = '1';
        headerList.style.overflow = 'visible';
        setAccInfoText();
    } else {
        // Wenn das Div ausgeblendet ist, wird es wieder eingeblendet
        //profilePage.style.display = 'none';
        headerList.style.height = '0px';
        headerList.style.opacity = '0';
        headerList.style.overflow = 'hidden';


    }
}

function editData() {

    makeFieldsWritable();
    

    var profileEditButtonsDisplay = document.getElementById('profileEditButtonsDisplay');

    profileEditButtonsDisplay.style.display = 'flex';




   


   


}

function doNotEditData() {

    var profileEditButtonsDisplay = document.getElementById('profileEditButtonsDisplay');

    profileEditButtonsDisplay.style.display = 'none';


    document.querySelector("#firstNameTF").disabled = true;
    document.querySelector("#lastNameTF").disabled = true;
    document.querySelector("#usernameTF").disabled = true;
    document.querySelector("#emailTF").disabled = true;


    setAccInfoText();

}

async function saveNewUserData() {
    const token = localStorage.getItem("jwt-token");
    firstName = document.querySelector("#firstNameTF").value;
    lastName = document.querySelector("#lastNameTF").value;
    username = document.querySelector("#usernameTF").value;
    email = document.querySelector("#emailTF").value;

    await changeCurrentUser(token, firstName, lastName, username, email);


    doNotEditData();
}




async function deleteAcc() {
    const token = localStorage.getItem("jwt-token");
    // Hier bekommst du das Token nach einer erfolgreichen Anmeldung

    try {
        const deleteResponse = await deleteAccount(token);
        console.log("Deleted User")
        console.log(deleteResponse);
    } catch (error) {
        console.error(error.message);
    }

}


function signOut() {
    localStorage.removeItem("jwt-token");
    toggleProfile();
}





async function setAccInfoText() {
    const token = localStorage.getItem("jwt-token");
    firstNameTF = document.querySelector("#firstNameTF");
    lastNameTF = document.querySelector("#lastNameTF");
    usernameTF = document.querySelector("#usernameTF");
    emailTF = document.querySelector("#emailTF");

    const currentUser = await getCurrentUser(token);
    



    firstNameTF.value = currentUser.firstName;
    lastNameTF.value = currentUser.familyName;
    usernameTF.value = currentUser.username;
    emailTF.value = currentUser.email;
    


    
}




function makeFieldsWritable() {
    document.querySelector("#firstNameTF").disabled = false;
    document.querySelector("#lastNameTF").disabled = false;
    document.querySelector("#usernameTF").disabled = false;
    document.querySelector("#emailTF").disabled = false;
}



