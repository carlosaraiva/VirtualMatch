export interface User {
    username: String;
    token: String;
    photoUrl: String;
    knownAs: string;
    gender: string;
}

export function newUser() : User {
    return {
        username: '',
        token: '',
        photoUrl: '',
        knownAs: '',
        gender: ''
    };
}