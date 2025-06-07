"use client";

import { Button } from "@/components/ui/button";
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage
} from "@/components/ui/form";
import { post } from "@/lib/apiClient";
import { zodResolver } from "@hookform/resolvers/zod";
import { BaseSyntheticEvent } from "react";
import { useForm } from "react-hook-form";
import { z } from "zod";

const loginFormSchema = z.object({
  email: z.string().email(),
  password: z.string().min(16),
});

export default function Page() {
  const loginForm = useForm<z.infer<typeof loginFormSchema>>({
    resolver: zodResolver(loginFormSchema),
    defaultValues: {
      email: "",
      password: "",
    },
  });

  const loginFormSubmit = async (values: z.infer<typeof loginFormSchema>) => {
    try {
        const response = await post('/auth', values);
        console.log(response);
    } catch (error) {
        console.error(error);
    }
  };

  return (
    <main>
      <div className="min-h-screen min-w-screen m-0 p-0 flex items-center justify-center">
        <section className="flex">
          <div className="border-1 p-3 rounded-2xl">
            <Form {...loginForm}>
              <form onSubmit={(e) => {e.preventDefault();loginForm.handleSubmit(loginFormSubmit)}}>
                <FormField
                  control={loginForm.control}
                  name="email"
                  render={({ field }) => (
                    <FormItem className="mb-4">
                      <FormLabel>Email</FormLabel>
                      <FormMessage />
                      <FormControl>
                        <input placeholder="Email" {...field} className="border-1 p-1 rounded-md" />
                      </FormControl>
                    </FormItem>
                  )}
                />

                <FormField
                  control={loginForm.control}
                  name="password"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Password</FormLabel>
                      <FormMessage />
                      <FormControl>
                        <input type="password" placeholder="Password" {...field} className="border-1 p-1 rounded-md" />
                      </FormControl>
                    </FormItem>
                  )}
                />

                  <button className="text-xs">Sign up instead</button>

                  <div className="flex justify-end mt-2">
                      <Button className="ml-auto cursor-pointer" type="submit">Sign in</Button>
                  </div>
              </form>
            </Form>
          </div>
        </section>
      </div>

      <section></section>
    </main>
  );
}
