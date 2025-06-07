"use client";

import desktopBackground from "../../../public/static/images/desktop-background.svg";
import logoDark from "../../../public/static/images/logo-dark.svg";

import Image from "next/image";

import { useRouter } from "next/navigation";

import { post } from "@/lib/apiClient";
import { z } from "zod";

import { LoginForm, LoginFormSchema } from "@/components/ui/login-form";
import { SignUpForm } from "@/components/ui/sign-up-form";
import { setAuthToken } from "@/lib/authService";
import { AnimatePresence } from "motion/react";
import { useState } from "react";

import { toast } from "react-toastify";

const showError = (error: string) => {
  toast.error(error, {
    position: "top-right",
    autoClose: 3000,
  });
};

export default function Page() {
  const [isLogin, setIsLogin] = useState(true);
  const { push } = useRouter();

  const loginFormSubmit = async (values: z.infer<typeof LoginFormSchema>) => {
    try {
      const response = await post("/auth", values);
      const { value: accessToken } = response;
      setAuthToken(accessToken);
      push("/dashboard");
    } catch {
      showError("An unkown error occurred. Please try again later.");
    }
  };

  const signUpFormSubmit = async (values: z.infer<typeof LoginFormSchema>) => {
    try {
      const response = await post("/user", values);
      if (!response.isSuccess) {
        const errorMessage: string = response.errors.reduce(
          (acc: string, error: string) => `${acc}${error}\n`,
          ""
        );
        showError(errorMessage);

        return;
      }
      setIsLogin(true);
    } catch {
      showError("An unkown error occurred. Please try again later.");
    }
  };

  return (
    <main
      style={{
        backgroundImage: `url(${desktopBackground.src})`,
        backgroundSize: "cover",
      }}
      className="relative"
    >
      <Image
        src={logoDark}
        alt="The enterprise logo"
        className="absolute top-5 right-5"
      />

      <div className="min-h-screen min-w-screen m-0 p-0 flex items-center justify-center">
        <section className="flex">
          <AnimatePresence mode="wait">
            {isLogin ? (
              <LoginForm
                onSubmit={loginFormSubmit}
                onFormChange={() => setIsLogin(false)}
              />
            ) : (
              <SignUpForm
                key="signup"
                onSubmit={signUpFormSubmit}
                onFormChange={() => setIsLogin(true)}
              />
            )}
          </AnimatePresence>
        </section>
      </div>
    </main>
  );
}
