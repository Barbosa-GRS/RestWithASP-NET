import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";

import Login from "./pages/Login";
import Books from "./pages/Books";
import NewBook from "./pages/NewBook";


export default function AppRoutes() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/"  element={<Login />} />
                <Route path="/books" element={<Books/>} />
                <Route path="/book/new" element={<NewBook/>} />
            </Routes>
        </BrowserRouter>
    );
}
