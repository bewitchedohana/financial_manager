import { zodResolver } from "@hookform/resolvers/zod";
import { useForm, FormProvider } from "react-hook-form";
import {
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "./form";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Button } from "./button";
import z from "zod";
import { faArrowRight } from "@fortawesome/free-solid-svg-icons";
import { motion } from "motion/react";

type FormTypes = {
  onSubmit: (values: z.infer<typeof LoginFormSchema>) => Promise<void>;
  onFormChange: () => void;
};

export const LoginFormSchema = z.object({
  email: z.string().email(),
  password: z.string().min(16),
});

export const LoginForm = ({ onSubmit, onFormChange }: FormTypes) => {
  const loginForm = useForm<z.infer<typeof LoginFormSchema>>({
    resolver: zodResolver(LoginFormSchema),
    defaultValues: {
      email: "",
      password: "",
    },
  });

  return (
    <motion.div  exit={{ opacity: 0, transform: "translateX(-100%)" }} className="border-1 rounded-2xl bg-white shadow-lg min-w-80 p-6">
      <FormProvider {...loginForm}>
        <form onSubmit={loginForm.handleSubmit(onSubmit)}>
          <FormField
            control={loginForm.control}
            name="email"
            render={({ field }) => (
              <FormItem className="mb-4">
                <FormLabel>Email</FormLabel>
                <FormMessage />
                <FormControl>
                  <input
                    placeholder="Email"
                    {...field}
                    className="border-1 p-1 rounded-md border-black"
                  />
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
                  <input
                    type="password"
                    placeholder="Password"
                    {...field}
                    className="border-1 p-1 rounded-md border-black"
                  />
                </FormControl>
              </FormItem>
            )}
          />

          <div className="flex justify-end">
            <button
              className="text-xs cursor-pointer my-2"
              type="button"
              onClick={onFormChange}
            >
              Sign up instead
              <FontAwesomeIcon icon={faArrowRight} className="ml-1" />
            </button>
          </div>

          <div className="flex justify-end mt-2">
            <Button
              className="ml-auto cursor-pointer bg-dn-orange hover:bg-yellow-400 text-black min-w-24"
              type="submit"
            >
              Sign in
            </Button>
          </div>
        </form>
      </FormProvider>
    </motion.div>
  );
};
