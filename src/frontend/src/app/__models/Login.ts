export interface Login {
    username: String;
    password: String;
    photoUrl: String;
}

export function newLogin() : Login {
    return {
        username: '',
        password: '',
        photoUrl: ''
    };
}