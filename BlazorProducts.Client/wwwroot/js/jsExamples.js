export function showAlert(message) {
    alert(message);
}

export function showAlertObject(person) {
    const message = `this person is called ${person.name} and is ${person.age} years old`;
    alert(message);
}

export function emailRegistration(message) {
    const result = prompt(message);

    if (!result)
        return 'Please provide an email to register';

    const returnMessage = `Thanks, your email ${result} has been registered correctly`;

    return returnMessage;
}