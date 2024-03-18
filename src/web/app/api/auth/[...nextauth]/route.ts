import NextAuth from 'next-auth';
import GoogleProvider from 'next-auth/providers/google';

export const authOptions = {
    providers: [
        GoogleProvider({
            clientId: process.env.GOOGLE_OAUTH_CLIENT_ID ?? '',
            clientSecret: process.env.GOOGLE_OAUTH_CLIENT_SECRET ?? '',
            profile(profile) {
                console.log('Profile Google: ', profile);

                let userRole = 'Google User';
                return {
                    ...profile,
                    id: profile.sub,
                    role: userRole,
                };
            },
        }),
    ],
    callbacks: {
        async signIn(params: any) {
            const { user, account, profile, email, credentials } = params;
            console.log('Google Profile:', profile, email, credentials);

            // try {
            //     const url = `${process.env.GATEWAY_BASE_ADDRESS}/api/authenticate?email=${profile.email}`;
            //     console.log(url);
            //     const response = await fetch(url, {
            //         method: 'POST',
            //         headers: {
            //             'Content-Type': 'application/json',
            //         },
            //         body: JSON.stringify({ profile }),
            //     });

            //     if (response.ok) {
            //         console.log('User data sent successfully to backend');
            //     } else {
            //         console.error('Failed to send user data to backend');
            //     }
            //     return true;
            // } catch (err) {
            //     console.log(err);
            //     return false;
            // }
			return true;
        },
    },
};

const handler = NextAuth(authOptions);
export { handler as GET, handler as POST };
