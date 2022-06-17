export interface Login {
    username: String;
    pass: String;
    photoUrl: String;
}

export function newLogin() : Login {
    return {
        username: '',
        pass: '',
        photoUrl: ''
    };
}