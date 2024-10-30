import React, {useState, useEffect, }from "react";  /* useEffect faz a tele buscar os dados para ser renderisada corretamente*/
import { Link , useNavigate} from "react-router-dom";
import { FiPower, FiEdit, FiTrash, FiTrash2 } from "react-icons/fi";

import api from '../../services/api'; /* importa a api */

import './styles.css'

import logoImage from '../../assets/logo.svg'

export default function Books() {

    const [books, setBooks] = useState([]);

    const userName = localStorage.getItem('userName'); /*recuperar o nome do usuario */

    const accessToken = localStorage.getItem('accessToken'); /* recupera o token */

    const navigate = useNavigate(); /*defini a navegação entre paginas  */

    useEffect(()=> {
        api.get('api/Book/v1/asc/5/1',  {   /* envia uma requisição post para o endpoin usando data na requisição para verificar se o token esta ativo*/
                headers: {
                    Authorization:`Bearer ${accessToken}`  /* identifica e autoriza o usuario usando o token*/
                }
        }).then(response=> {
            setBooks(response.data.list)
        })           
    },[accessToken]); /*sempre que mudar ele deve verificar o token*/

    return (
        <div className="book-container">
            <header>
                <img src ={logoImage} alt="Erudio"/>
                <span>Welcome, <strong>{userName.toLowerCase()}</strong>!</span>
                <Link className="button" to="/book/new">Add New Book</Link>
                <button type="button">
                    <FiPower size= {18} color="#251fc5"/>             
                </button>
            </header>
            <h1>Registered Books</h1>
            <ul>
                {books.map(book =>(
                    <li>
                        <strong>Title:</strong>
                        <p>{book.title}</p>
                        <strong>Author:</strong>
                        <p>{book.author}</p>
                        <strong>Price:</strong>
                        <p>{book.price}</p>
                        <strong>Realease Date:</strong>
                        <p>{book.launchDate}</p>

                        <button type="button">
                            <FiEdit size={20} color="#251f5"/>
                        </button>  
                        
                        <button type="button">
                            <FiTrash2 size={20} color="#251f5"/>
                        </button>                  
                    </li>
                ))}
            </ul>
        </div>
    );
}