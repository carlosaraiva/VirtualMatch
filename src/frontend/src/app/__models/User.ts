export interface User {
    username: String;
    token: String;
    photoUrl: String;
}

export function newUser() : User {
    return {
        username: '',
        token: '',
        photoUrl: ''
    };
}