'use client';
import {useEffect, useState} from "react";
import {variables} from "@/constants/variables";
import {useAuth} from "@/hooks/useAuth";
import useUser from "@/hooks/useUser";
import Header from "@/components/Header";

export default function Page() {
    const isAuthenticated = useAuth();

    return (
        <section>
            <Header />
        </section>
    )
}
