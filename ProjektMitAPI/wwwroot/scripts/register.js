async function submitRegisterForm(e) {
    e.preventDefault();

    const firstName = document.querySelector('#firstName').value;
    const familyName = document.querySelector('#familyName').value;
    const userName = document.querySelector('#userName').value;
    const email = document.querySelector('#email').value;
    const password = document.querySelector('#password').value;
    
    try {
        const loginInfo = await register(firstName, familyName, userName, email, password);
        localStorage.setItem('jwt-token', loginInfo.jwt);
        window.location.href = '../';
    }
    catch (err) {
        document.querySelector('#register-error').innerText = err.message;
    }
}

document.querySelector('form').addEventListener('submit', submitRegisterForm);