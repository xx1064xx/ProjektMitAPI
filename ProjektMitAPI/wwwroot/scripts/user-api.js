async function login(email, password) {
    const request = await fetch(`https://localhost:7072/api/users/login`, {
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        cache: 'no-cache',
        method: 'POST',
        body: JSON.stringify({ 'email': email, 'password': password })
    });
    const data = await request.json();
    return data;
}

async function register(firstName, familyName, userName, email, password) {
    const request = await fetch(`https://localhost:7072/api/users/register`, {
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        cache: 'no-cache',
        method: 'POST',
        body: JSON.stringify({'firstName': firstName, 'familyName': familyName, 'userName': userName, 'email': email, 'password': password })
    });
    const data = await request.json();
    return data;
}



async function deleteAccount(token) {
    const request = await fetch(`https://localhost:7072/api/users/delete`, {
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        cache: 'no-cache',
        method: 'DELETE'
    });

    if (request.ok) {
        const data = await request.json();
        localStorage.removeItem("jwt-token");
        window.location.href = '/login';
        return data;
    } else {
        const errorData = await request.json();
        throw new Error(errorData.message);
    }
}


async function getCurrentUser(token) {
    const request = await fetch(`https://localhost:7072/api/users/getCurrentUser`, {
        headers: {
            'Accept': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        method: 'GET'
    });
    const data = await request.json();
    return data; 
}



async function changeCurrentUser(token, firstName, familyName, userName, email) {
    console.log("Token: " + token);
    console.log("First Name: " + firstName);
    console.log("Last Name: " + lastName);
    console.log("Username: " + username);
    console.log("Email: " + email);
    const request = await fetch(`https://localhost:7072/api/users/changeCurrentUser`, {
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        cache: 'no-cache',
        method: 'PUT',
        body: JSON.stringify({ 'firstName': firstName, 'familyName': familyName, 'userName': userName, 'email': email, 'password': "NONE" })
    });
    const data = await request.json();
    return data;
}