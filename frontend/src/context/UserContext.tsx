'use client';
import React, { createContext, useContext, useState, ReactNode } from 'react';

interface User {
    cpfCnpj: string;
    name: string;
    email: string;
}

interface UserContextType {
    user: User | null;
    login: (data: { account: User; token: string }) => void;
    logout: () => void;
    createAccount: (data: User) => void;
}

interface UserProviderProps {
    children: ReactNode;
}

export const UserContext = createContext<UserContextType | undefined>(undefined);

export function UserProvider({ children }: UserProviderProps) {
    const [user, setUser] = useState<User | null>(null);

    const createAccount = (data: User) => {
        setUser(data);
    };

    const login = (data: { account: User; token: string }) => {
        setUser(data.account);
        localStorage.setItem('token', data.token);
    };

    const logout = () => {
        setUser(null);
        localStorage.removeItem('token');
    };

    return (
        <UserContext.Provider value={{ user, login, logout, createAccount }}>
            {children}
        </UserContext.Provider>
    );
}
