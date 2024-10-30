import React ,  {useState} from "react";
import { Link, useNavigate } from "react-router-dom";   
import { FiArrowLeft } from "react-icons/fi";

import api from '../../services/api'; /* importa a api */


import './styles.css'  /* importa o css q faz a aparencia da pagina */

import logoImage from '../../assets/logo.svg' /* importa imagem da logo */

export default function NewBook(){   /* define os itens de um novo livro */

    const [author, setAuthor] = useState('');
    const [title, setTitle] = useState('');
    const [launchDate, setLaunchDate] = useState('');
    const [price, setPrice] = useState('');

    const navigate = useNavigate(); /*defini a navegação entre paginas  */


    async function createNewBook(e){ /* cria um novo livro */
        e.preventDefault(); /* evita refresh da pagina e mantem o SPA */

        const data = {
            title,
            author,
            launchDate,
            price,
        }

        const accessToken = localStorage.getItem('accessToken');

        try {
            await api.post('api/Book/v1', data, {
                headers: {
                    Authorization:`Bearer ${accessToken}`
                }

            });
        } catch (error) {
            alert('Error while recording a book! Try again!')
        }

        navigate('/books') /* volta para a listagem de books */
    }

    return(
        <div className="new-book-container">
            <div className="content">
                <section className="form">
                    <img src={logoImage} alt="Erudio"/>
                    <h1>Add New Book</h1>
                    <p>Enter the book information and click on 'Add'!</p>
                    <Link className="back-link" to = "/books">
                        <FiArrowLeft size={16} color="#251fc5"/>
                    Home
                    </Link>
                </section>
                <form onSubmit={createNewBook}>
                    <input 
                        placeholder="Title" 
                        value={title}
                        onChange={e => setTitle(e.target.value)} 
                    />
                    <input 
                        placeholder="Author" 
                        value={author}
                        onChange={e => setAuthor(e.target.value)} 
                    />
                    <input 
                        type="Date" 
                        value={launchDate}
                        onChange={e => setLaunchDate(e.target.value)} 
                    />
                    <input 
                        placeholder="Price" 
                        value={price}
                        onChange={e => setPrice(e.target.value)} 
                    />
                    

                    <button className="button" type="submit">Add</button>
                </form>
            </div>
        </div>
    );
}