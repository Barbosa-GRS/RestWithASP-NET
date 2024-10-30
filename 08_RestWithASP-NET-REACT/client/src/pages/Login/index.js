import React, {useState}from 'react';

import {Navigate, useNavigate} from 'react-router-dom'

import api from '../../services/api';   

import "./styles.css";

import logoImage from '../../assets/logo.svg'
import padlock from '../../assets/padlock.png'


export default function Login() {

    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');

    const navigate = useNavigate();

    async function login(e){
        e.preventDefault();

        const data = {
            userName,
            password,
        };

            try {
                const response = await api.post('api/auth/v1/signin', data)

                localStorage.setItem('userName' , userName);
                localStorage.setItem('accessToken' , response.data.accessToken);
                localStorage.setItem('refreshToken' , response.data.refreshToken);

                navigate('/books');

            } catch (error) {
                alert('Login falied! Try again!');
                
            }
        
    }

    return (
        <div className="login-containner">
            <section className="from">
            <img src={logoImage} alt="Erudio Logo" />
            <form onSubmit={login}>
                <h1>Acess your account</h1>

                <input 
                    placeholder = "Username"
                    value={userName}
                    onChange={e => setUserName(e.target.value)}
                />
                <input 
                    type="password" placeholder = "Password"
                    value={password}
                    onChange={e => setPassword(e.target.value)}
                />

                <button className='button' type="submit">Login</button>
            </form>

            </section>

            <img src={padlock} alt="Login" />

        </div>
            
    )
}