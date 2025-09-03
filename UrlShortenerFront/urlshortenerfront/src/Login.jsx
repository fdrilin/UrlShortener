import './index.css'
import { useState } from 'react'
import { simpleFetch } from './Fetch.js'
import { fetchTypes, Enums } from "./Enums.js"

function Login() {
    // eslint-disable-next-line no-unused-vars
    const [error, setError] = useState("");

    return (
        <>
            <form onSubmit={(e) => { e.preventDefault(); }}>
                <label className='labelInput'>Username</label><input name="username" />
                <label className='labelInput'>Password</label><input type='password' name="password" />
                <div className='ErrorDiv'><span>{error}</span></div>
                <div>
                    <button className='button-save' onClick={() => save("login", setError)}>Login</button>
                    <button className='button-save' onClick={() => save("register", setError)}>Register</button>
                </div>
            </form>
        </>
    )
}

function save(type, setError) {
    let values = [
        document.querySelector("form input[name=username]").value,
        document.querySelector("form input[name=password]").value
    ]
    simpleFetch(fetchTypes.Post, "Auth/" + type, ["Username", "Password"], values, (res) => {
        let mes = res.message.split(" ");
        sessionStorage.setItem(Enums.currentUser, mes[0]);
        sessionStorage.setItem(Enums.currentUserId, mes[1]);
        window.location.href = '/';
    }, (err) => {
        setError(err);
    })
}

export default Login