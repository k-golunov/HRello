import ConsoleAndPhoto from "../components/ConsoleAndPhoto/ConsoleAndPhoto";
import InfoBlock from "../components/InfoBlock/InfoBlock";
import s from "./Pages.module.css";
import {Link, useNavigate} from "react-router-dom";
import Input from "../components/Input/Input";
import RegistrationForm from "../components/RegistrationForm/RegistrationForm";
import NavigateButton from "../components/NavigateButton/NavigateButton";
import {useDispatch} from "react-redux";
import {removeUser} from "../store/slices/userSlice";
import {removeProfile} from "../store/slices/profileSlice";

export const NotActivateAccount = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const logout = () => {
        debugger
        dispatch(removeProfile());
        dispatch(removeUser());
        navigate("/");
    };
    return (
        <div>
            <div className={s.errorPage}>
                <svg width="637" height="82" viewBox="0 0 637 82" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <line x1="68" y1="26" x2="188" y2="26" stroke="#81E6D9" stroke-width="2"/>
                    <line x1="241" y1="26" x2="361" y2="26" stroke="#4F4F53" stroke-width="2"/>
                    <line x1="413" y1="26" x2="533" y2="26" stroke="#4F4F53" stroke-width="2"/>
                    <circle cx="42" cy="27" r="26" fill="#81E6D9" stroke="#81E6D9" stroke-width="2"/>
                    <path d="M44.1637 20.9091V34H41.7923V23.2166H41.7156L38.6538 25.1726V22.9993L41.9073 20.9091H44.1637Z" fill="#202023"/>
                    <path d="M0.984375 67.1357V77H2.74805V73.7393H4.74414C6.74023 73.7393 8.1416 72.4062 8.1416 70.4375C8.1416 68.4688 6.78125 67.1357 4.81934 67.1357H0.984375ZM2.74805 68.5781H4.35449C5.61914 68.5781 6.35059 69.2549 6.35059 70.4375C6.35059 71.6338 5.6123 72.3174 4.34766 72.3174H2.74805V68.5781ZM12.332 70.9229C13.3096 70.9229 13.9658 71.6406 14 72.666H10.6162C10.6846 71.6543 11.375 70.9229 12.332 70.9229ZM14.0068 74.833C13.8223 75.4346 13.2207 75.8311 12.4277 75.8311C11.3135 75.8311 10.6025 75.0518 10.6025 73.8896V73.7871H15.668V73.2334C15.668 71.0322 14.3828 69.6035 12.3252 69.6035C10.2334 69.6035 8.90723 71.1143 8.90723 73.4043C8.90723 75.7148 10.2197 77.1436 12.3936 77.1436C14.1094 77.1436 15.3809 76.2002 15.5996 74.833H14.0068ZM21.7998 71.0664V69.7539H16.8848V77H18.5801V71.0664H21.7998ZM24.6641 77L27.8154 72.126H27.8701V77H29.5381V69.7539H27.8701L24.7188 74.5938H24.6641V69.7539H22.9961V77H24.6641ZM37.3994 72.1396C37.2012 70.7246 36.0459 69.6035 34.2207 69.6035C32.1152 69.6035 30.7686 71.0801 30.7686 73.377C30.7686 75.708 32.1152 77.1436 34.2344 77.1436C36.0049 77.1436 37.208 76.1387 37.4131 74.6211H35.7998C35.6084 75.3457 35.0684 75.7695 34.248 75.7695C33.1748 75.7695 32.4844 74.874 32.4844 73.377C32.4844 71.9072 33.168 70.9844 34.2412 70.9844C35.0889 70.9844 35.6084 71.4902 35.7861 72.1396H37.3994ZM44.0645 69.7539H37.9189V71.0664H40.1475V77H41.8359V71.0664H44.0645V69.7539ZM49.1025 69.6309C48.0498 69.6309 47.2295 70.1641 46.8193 71.0049H46.7852V69.7539H45.124V79.3857H46.8262V75.7969H46.8604C47.2568 76.6104 48.0771 77.1162 49.1299 77.1162C50.9482 77.1162 52.1104 75.6875 52.1104 73.377C52.1104 71.0596 50.9414 69.6309 49.1025 69.6309ZM48.583 75.7217C47.5234 75.7217 46.8193 74.7988 46.8125 73.377C46.8193 71.9688 47.5234 71.0322 48.583 71.0322C49.6836 71.0322 50.3672 71.9482 50.3672 73.377C50.3672 74.8125 49.6836 75.7217 48.583 75.7217ZM56 75.8311C55.2617 75.8311 54.7695 75.4551 54.7695 74.8604C54.7695 74.2861 55.2412 73.917 56.0615 73.8623L57.7363 73.7598V74.3135C57.7363 75.1885 56.9639 75.8311 56 75.8311ZM55.4873 77.1162C56.417 77.1162 57.3262 76.6309 57.7432 75.8447H57.7773V77H59.418V72.0098C59.418 70.5537 58.249 69.6035 56.4512 69.6035C54.6055 69.6035 53.4502 70.5742 53.375 71.9277H54.9541C55.0635 71.3262 55.5762 70.9365 56.3828 70.9365C57.2236 70.9365 57.7363 71.374 57.7363 72.1328V72.6523L55.8223 72.7617C54.0586 72.8711 53.0674 73.6436 53.0674 74.9287C53.0674 76.2344 54.0859 77.1162 55.4873 77.1162ZM68.3662 78.8936V75.6875H67.252V69.7539H65.5566V75.6875H62.6035V69.7539H60.9082V77H66.8076V78.8936H68.3662ZM70.998 77L74.1494 72.126H74.2041V77H75.8721V69.7539H74.2041L71.0527 74.5938H70.998V69.7539H69.3301V77H70.998ZM80.2607 73.3291C79.4814 73.3291 78.9824 72.8643 78.9824 72.2217C78.9824 71.5039 79.4814 71.0527 80.2607 71.0527H81.5186V73.3291H80.2607ZM81.5186 77H83.1865V69.7539H80.1309C78.4082 69.7539 77.2939 70.7041 77.2939 72.2285C77.2939 73.2539 77.9023 73.999 78.9072 74.3066L76.959 77H78.8594L80.5479 74.5732H81.5186V77Z" fill="white"/>
                    <path d="M210.074 34V32.2869L214.618 27.8317C215.053 27.3928 215.415 27.0028 215.705 26.6619C215.995 26.321 216.212 25.9908 216.357 25.6712C216.502 25.3516 216.574 25.0107 216.574 24.6484C216.574 24.2351 216.48 23.8814 216.293 23.5874C216.105 23.2891 215.848 23.0589 215.52 22.897C215.191 22.7351 214.819 22.6541 214.401 22.6541C213.971 22.6541 213.593 22.7436 213.27 22.9226C212.946 23.0973 212.694 23.3466 212.515 23.6705C212.341 23.9943 212.253 24.38 212.253 24.8274H209.997C209.997 23.9964 210.186 23.2741 210.566 22.6605C210.945 22.0469 211.467 21.5717 212.132 21.2351C212.801 20.8984 213.568 20.7301 214.433 20.7301C215.311 20.7301 216.082 20.8942 216.747 21.2223C217.412 21.5504 217.927 22 218.294 22.571C218.664 23.142 218.85 23.794 218.85 24.527C218.85 25.017 218.756 25.4986 218.569 25.9716C218.381 26.4446 218.051 26.9687 217.578 27.544C217.109 28.1193 216.451 28.8161 215.603 29.6342L213.346 31.929V32.0185H219.048V34H210.074Z" fill="#81E6D9"/>
                    <circle cx="214.5" cy="27" r="26" stroke="#81E6D9" stroke-width="2"/>
                    <path d="M155.711 77H157.57L154.07 67.1357H152.081L148.581 77H150.399L151.24 74.4502H154.87L155.711 77ZM153.031 68.8789H153.079L154.453 73.0898H151.65L153.031 68.8789ZM161.904 73.1377L164.974 69.7539H162.991L160.339 72.7754H160.284V69.7539H158.589V77H160.284V73.7598H160.339L163.032 77H165.117L161.904 73.1377ZM171.481 69.7539H165.336V71.0664H167.564V77H169.253V71.0664H171.481V69.7539ZM174.202 77L177.354 72.126H177.408V77H179.076V69.7539H177.408L174.257 74.5938H174.202V69.7539H172.534V77H174.202ZM183.643 70.9092C184.333 70.9092 184.716 71.2305 184.716 71.7979C184.716 72.3994 184.306 72.7139 183.513 72.7139H182.269V70.9092H183.643ZM183.615 73.7939C184.531 73.7939 185.003 74.1426 185.003 74.8193C185.003 75.4824 184.559 75.8447 183.752 75.8447H182.269V73.7939H183.615ZM180.601 77H184.094C185.7 77 186.698 76.1865 186.698 74.9082C186.698 73.999 186.056 73.3291 185.105 73.2061V73.1514C185.823 73.0283 186.377 72.3516 186.377 71.5928C186.377 70.4443 185.502 69.7539 184.039 69.7539H180.601V77ZM190.608 75.8311C189.87 75.8311 189.378 75.4551 189.378 74.8604C189.378 74.2861 189.85 73.917 190.67 73.8623L192.345 73.7598V74.3135C192.345 75.1885 191.572 75.8311 190.608 75.8311ZM190.096 77.1162C191.025 77.1162 191.935 76.6309 192.352 75.8447H192.386V77H194.026V72.0098C194.026 70.5537 192.857 69.6035 191.06 69.6035C189.214 69.6035 188.059 70.5742 187.983 71.9277H189.562C189.672 71.3262 190.185 70.9365 190.991 70.9365C191.832 70.9365 192.345 71.374 192.345 72.1328V72.6523L190.431 72.7617C188.667 72.8711 187.676 73.6436 187.676 74.9287C187.676 76.2344 188.694 77.1162 190.096 77.1162ZM202.975 78.8936V75.6875H201.86V69.7539H200.165V75.6875H197.212V69.7539H195.517V77H201.416V78.8936H202.975ZM205.606 77L208.758 72.126H208.812V77H210.48V69.7539H208.812L205.661 74.5938H205.606V69.7539H203.938V77H205.606ZM214.869 73.3291C214.09 73.3291 213.591 72.8643 213.591 72.2217C213.591 71.5039 214.09 71.0527 214.869 71.0527H216.127V73.3291H214.869ZM216.127 77H217.795V69.7539H214.739C213.017 69.7539 211.902 70.7041 211.902 72.2285C211.902 73.2539 212.511 73.999 213.516 74.3066L211.567 77H213.468L215.156 74.5732H216.127V77ZM224.774 75.8311C224.036 75.8311 223.544 75.4551 223.544 74.8604C223.544 74.2861 224.016 73.917 224.836 73.8623L226.511 73.7598V74.3135C226.511 75.1885 225.738 75.8311 224.774 75.8311ZM224.262 77.1162C225.191 77.1162 226.101 76.6309 226.518 75.8447H226.552V77H228.192V72.0098C228.192 70.5537 227.023 69.6035 225.226 69.6035C223.38 69.6035 222.225 70.5742 222.149 71.9277H223.729C223.838 71.3262 224.351 70.9365 225.157 70.9365C225.998 70.9365 226.511 71.374 226.511 72.1328V72.6523L224.597 72.7617C222.833 72.8711 221.842 73.6436 221.842 74.9287C221.842 76.2344 222.86 77.1162 224.262 77.1162ZM232.998 73.1377L236.067 69.7539H234.085L231.433 72.7754H231.378V69.7539H229.683V77H231.378V73.7598H231.433L234.126 77H236.211L232.998 73.1377ZM240.395 73.1377L243.464 69.7539H241.481L238.829 72.7754H238.774V69.7539H237.079V77H238.774V73.7598H238.829L241.522 77H243.607L240.395 73.1377ZM246.991 75.8311C246.253 75.8311 245.761 75.4551 245.761 74.8604C245.761 74.2861 246.232 73.917 247.053 73.8623L248.728 73.7598V74.3135C248.728 75.1885 247.955 75.8311 246.991 75.8311ZM246.479 77.1162C247.408 77.1162 248.317 76.6309 248.734 75.8447H248.769V77H250.409V72.0098C250.409 70.5537 249.24 69.6035 247.442 69.6035C245.597 69.6035 244.441 70.5742 244.366 71.9277H245.945C246.055 71.3262 246.567 70.9365 247.374 70.9365C248.215 70.9365 248.728 71.374 248.728 72.1328V72.6523L246.813 72.7617C245.05 72.8711 244.059 73.6436 244.059 74.9287C244.059 76.2344 245.077 77.1162 246.479 77.1162ZM252.713 79.6934C254.367 79.6934 255.167 79.085 255.789 77.2871L258.407 69.7539H256.616L254.907 75.5508H254.88L253.171 69.7539H251.312L253.889 77.0068C253.896 77.0273 253.793 77.3896 253.793 77.4102C253.602 78.0801 253.239 78.3467 252.535 78.3467C252.433 78.3467 252.132 78.3398 252.043 78.3193V79.666C252.132 79.6865 252.617 79.6934 252.713 79.6934ZM264.108 77H265.804V69.7539H264.108V72.6113H261.046V69.7539H259.351V77H261.046V73.9238H264.108V77ZM273.009 69.7539H266.863V71.0664H269.092V77H270.78V71.0664H273.009V69.7539ZM276.522 75.8311C275.784 75.8311 275.292 75.4551 275.292 74.8604C275.292 74.2861 275.764 73.917 276.584 73.8623L278.259 73.7598V74.3135C278.259 75.1885 277.486 75.8311 276.522 75.8311ZM276.01 77.1162C276.939 77.1162 277.849 76.6309 278.266 75.8447H278.3V77H279.94V72.0098C279.94 70.5537 278.771 69.6035 276.974 69.6035C275.128 69.6035 273.973 70.5742 273.897 71.9277H275.477C275.586 71.3262 276.099 70.9365 276.905 70.9365C277.746 70.9365 278.259 71.374 278.259 72.1328V72.6523L276.345 72.7617C274.581 72.8711 273.59 73.6436 273.59 74.9287C273.59 76.2344 274.608 77.1162 276.01 77.1162Z" fill="white"/>
                    <path d="M386.999 34.179C386.079 34.179 385.261 34.0213 384.545 33.706C383.833 33.3906 383.271 32.9517 382.857 32.3892C382.444 31.8267 382.224 31.1768 382.199 30.4396H384.602C384.623 30.7933 384.741 31.1023 384.954 31.3665C385.167 31.6264 385.45 31.8288 385.804 31.9737C386.158 32.1186 386.554 32.1911 386.993 32.1911C387.462 32.1911 387.877 32.1101 388.239 31.9482C388.601 31.782 388.885 31.5518 389.089 31.2578C389.294 30.9638 389.394 30.625 389.39 30.2415C389.394 29.8452 389.292 29.4957 389.083 29.1932C388.874 28.8906 388.572 28.6541 388.175 28.4837C387.783 28.3132 387.31 28.228 386.756 28.228H385.599V26.3999H386.756C387.212 26.3999 387.611 26.321 387.952 26.1634C388.297 26.0057 388.567 25.7841 388.763 25.4986C388.959 25.2088 389.055 24.8743 389.051 24.495C389.055 24.1243 388.972 23.8026 388.802 23.5298C388.636 23.2528 388.399 23.0376 388.092 22.8842C387.79 22.7308 387.434 22.6541 387.025 22.6541C386.624 22.6541 386.253 22.7266 385.913 22.8714C385.572 23.0163 385.297 23.223 385.088 23.4915C384.879 23.7557 384.768 24.071 384.756 24.4375H382.474C382.491 23.7045 382.702 23.0611 383.106 22.5071C383.516 21.9489 384.061 21.5142 384.743 21.2031C385.425 20.8878 386.19 20.7301 387.038 20.7301C387.911 20.7301 388.67 20.8942 389.313 21.2223C389.961 21.5462 390.462 21.983 390.815 22.5327C391.169 23.0824 391.346 23.6896 391.346 24.3544C391.35 25.0916 391.133 25.7095 390.694 26.2081C390.259 26.7067 389.688 27.0327 388.981 27.1861V27.2884C389.901 27.4162 390.606 27.7571 391.097 28.3111C391.591 28.8608 391.836 29.5447 391.832 30.3629C391.832 31.0959 391.623 31.7521 391.205 32.3317C390.792 32.907 390.221 33.3587 389.492 33.6868C388.768 34.0149 387.937 34.179 386.999 34.179Z" fill="#C2C2C2"/>
                    <circle cx="387" cy="27" r="26" stroke="#4F4F53" stroke-width="2"/>
                    <path d="M320.213 72.6729H321.56C322.797 72.6729 323.535 73.2402 323.535 74.1973C323.535 75.1406 322.756 75.7832 321.601 75.7832C320.397 75.7832 319.577 75.2021 319.502 74.2793H317.807C317.896 76.0703 319.399 77.2461 321.594 77.2461C323.747 77.2461 325.347 75.9883 325.347 74.2998C325.347 72.9736 324.52 72.0303 323.193 71.873V71.8184C324.287 71.5723 325.039 70.6562 325.039 69.5488C325.039 67.9834 323.645 66.8896 321.628 66.8896C319.509 66.8896 318.135 68.0176 318.046 69.8154H319.693C319.769 68.8857 320.486 68.3115 321.58 68.3115C322.66 68.3115 323.33 68.8584 323.33 69.7471C323.33 70.6357 322.599 71.251 321.525 71.251H320.213V72.6729ZM329.264 75.8311C328.525 75.8311 328.033 75.4551 328.033 74.8604C328.033 74.2861 328.505 73.917 329.325 73.8623L331 73.7598V74.3135C331 75.1885 330.228 75.8311 329.264 75.8311ZM328.751 77.1162C329.681 77.1162 330.59 76.6309 331.007 75.8447H331.041V77H332.682V72.0098C332.682 70.5537 331.513 69.6035 329.715 69.6035C327.869 69.6035 326.714 70.5742 326.639 71.9277H328.218C328.327 71.3262 328.84 70.9365 329.646 70.9365C330.487 70.9365 331 71.374 331 72.1328V72.6523L329.086 72.7617C327.322 72.8711 326.331 73.6436 326.331 74.9287C326.331 76.2344 327.35 77.1162 328.751 77.1162ZM338.861 77H340.563V69.7539H334.172V77H335.867V71.0664H338.861V77ZM345.301 77.1436C347.427 77.1436 348.808 75.7285 348.808 73.377C348.808 71.0322 347.413 69.6035 345.301 69.6035C343.188 69.6035 341.794 71.0391 341.794 73.377C341.794 75.7285 343.175 77.1436 345.301 77.1436ZM345.301 75.7969C344.221 75.7969 343.523 74.9219 343.523 73.377C343.523 71.8389 344.228 70.957 345.301 70.957C346.381 70.957 347.078 71.8389 347.078 73.377C347.078 74.9219 346.381 75.7969 345.301 75.7969ZM352.164 73.623L352.335 71.0664H354.399V77H356.095V69.7539H350.865L350.633 73.5342C350.564 74.7373 350.332 75.585 349.628 75.585C349.437 75.585 349.293 75.5508 349.225 75.5303V76.9932C349.313 77.0273 349.512 77.0752 349.778 77.0752C351.48 77.0752 352.027 75.7217 352.164 73.623ZM362.377 77H364.072V69.7539H362.377V72.6113H359.314V69.7539H357.619V77H359.314V73.9238H362.377V77ZM368.734 70.9229C369.712 70.9229 370.368 71.6406 370.402 72.666H367.019C367.087 71.6543 367.777 70.9229 368.734 70.9229ZM370.409 74.833C370.225 75.4346 369.623 75.8311 368.83 75.8311C367.716 75.8311 367.005 75.0518 367.005 73.8896V73.7871H372.07V73.2334C372.07 71.0322 370.785 69.6035 368.728 69.6035C366.636 69.6035 365.31 71.1143 365.31 73.4043C365.31 75.7148 366.622 77.1436 368.796 77.1436C370.512 77.1436 371.783 76.2002 372.002 74.833H370.409ZM378.045 77H379.74V69.7539H378.045V72.6113H374.982V69.7539H373.287V77H374.982V73.9238H378.045V77ZM382.939 77L386.091 72.126H386.146V77H387.813V69.7539H386.146L382.994 74.5938H382.939V69.7539H381.271V77H382.939ZM392.469 70.9229C393.446 70.9229 394.103 71.6406 394.137 72.666H390.753C390.821 71.6543 391.512 70.9229 392.469 70.9229ZM394.144 74.833C393.959 75.4346 393.357 75.8311 392.564 75.8311C391.45 75.8311 390.739 75.0518 390.739 73.8896V73.7871H395.805V73.2334C395.805 71.0322 394.52 69.6035 392.462 69.6035C390.37 69.6035 389.044 71.1143 389.044 73.4043C389.044 75.7148 390.356 77.1436 392.53 77.1436C394.246 77.1436 395.518 76.2002 395.736 74.833H394.144ZM404.514 77H406.216V69.7539H399.824V77H401.52V71.0664H404.514V77ZM411.726 69.6309C410.673 69.6309 409.853 70.1641 409.442 71.0049H409.408V69.7539H407.747V79.3857H409.449V75.7969H409.483C409.88 76.6104 410.7 77.1162 411.753 77.1162C413.571 77.1162 414.733 75.6875 414.733 73.377C414.733 71.0596 413.564 69.6309 411.726 69.6309ZM411.206 75.7217C410.146 75.7217 409.442 74.7988 409.436 73.377C409.442 71.9688 410.146 71.0322 411.206 71.0322C412.307 71.0322 412.99 71.9482 412.99 73.377C412.99 74.8125 412.307 75.7217 411.206 75.7217ZM419.184 77.1436C421.31 77.1436 422.69 75.7285 422.69 73.377C422.69 71.0322 421.296 69.6035 419.184 69.6035C417.071 69.6035 415.677 71.0391 415.677 73.377C415.677 75.7285 417.058 77.1436 419.184 77.1436ZM419.184 75.7969C418.104 75.7969 417.406 74.9219 417.406 73.377C417.406 71.8389 418.11 70.957 419.184 70.957C420.264 70.957 420.961 71.8389 420.961 73.377C420.961 74.9219 420.264 75.7969 419.184 75.7969ZM427.489 75.7764C426.17 75.7354 425.336 74.8535 425.336 73.377C425.336 71.9072 426.177 71.0117 427.489 70.9775V75.7764ZM431.283 73.377C431.283 74.8467 430.436 75.7422 429.123 75.7764V70.9775C430.442 71.0186 431.283 71.9004 431.283 73.377ZM427.482 77.1025V79.3857H429.137V77.1025C431.461 77.0547 432.999 75.626 432.999 73.377C432.999 71.1074 431.475 69.6992 429.137 69.6514V67.4229H427.482V69.6514C425.145 69.6992 423.62 71.1211 423.62 73.377C423.62 75.6465 425.131 77.0479 427.482 77.1025ZM435.891 77L439.042 72.126H439.097V77H440.765V69.7539H439.097L435.945 74.5938H435.891V69.7539H434.223V77H435.891ZM444.682 73.623L444.853 71.0664H446.917V77H448.612V69.7539H443.383L443.15 73.5342C443.082 74.7373 442.85 75.585 442.146 75.585C441.954 75.585 441.811 75.5508 441.742 75.5303V76.9932C441.831 77.0273 442.029 77.0752 442.296 77.0752C443.998 77.0752 444.545 75.7217 444.682 73.623ZM453.001 73.3291C452.222 73.3291 451.723 72.8643 451.723 72.2217C451.723 71.5039 452.222 71.0527 453.001 71.0527H454.259V73.3291H453.001ZM454.259 77H455.927V69.7539H452.871C451.148 69.7539 450.034 70.7041 450.034 72.2285C450.034 73.2539 450.643 73.999 451.647 74.3066L449.699 77H451.6L453.288 74.5732H454.259V77Z" fill="white"/>
                    <path d="M554.433 31.571V29.6854L559.987 20.9091H561.56V23.5938H560.601L556.862 29.5192V29.6214H564.615V31.571H554.433ZM560.678 34V30.9957L560.703 30.152V20.9091H562.941V34H560.678Z" fill="#C2C2C2"/>
                    <circle cx="559.5" cy="27" r="26" stroke="#4F4F53" stroke-width="2"/>
                    <path d="M485.378 72.6729H486.725C487.962 72.6729 488.7 73.2402 488.7 74.1973C488.7 75.1406 487.921 75.7832 486.766 75.7832C485.562 75.7832 484.742 75.2021 484.667 74.2793H482.972C483.061 76.0703 484.564 77.2461 486.759 77.2461C488.912 77.2461 490.512 75.9883 490.512 74.2998C490.512 72.9736 489.685 72.0303 488.358 71.873V71.8184C489.452 71.5723 490.204 70.6562 490.204 69.5488C490.204 67.9834 488.81 66.8896 486.793 66.8896C484.674 66.8896 483.3 68.0176 483.211 69.8154H484.858C484.934 68.8857 485.651 68.3115 486.745 68.3115C487.825 68.3115 488.495 68.8584 488.495 69.7471C488.495 70.6357 487.764 71.251 486.69 71.251H485.378V72.6729ZM494.429 75.8311C493.69 75.8311 493.198 75.4551 493.198 74.8604C493.198 74.2861 493.67 73.917 494.49 73.8623L496.165 73.7598V74.3135C496.165 75.1885 495.393 75.8311 494.429 75.8311ZM493.916 77.1162C494.846 77.1162 495.755 76.6309 496.172 75.8447H496.206V77H497.847V72.0098C497.847 70.5537 496.678 69.6035 494.88 69.6035C493.034 69.6035 491.879 70.5742 491.804 71.9277H493.383C493.492 71.3262 494.005 70.9365 494.812 70.9365C495.652 70.9365 496.165 71.374 496.165 72.1328V72.6523L494.251 72.7617C492.487 72.8711 491.496 73.6436 491.496 74.9287C491.496 76.2344 492.515 77.1162 493.916 77.1162ZM504.026 77H505.729V69.7539H499.337V77H501.032V71.0664H504.026V77ZM510.466 77.1436C512.592 77.1436 513.973 75.7285 513.973 73.377C513.973 71.0322 512.578 69.6035 510.466 69.6035C508.354 69.6035 506.959 71.0391 506.959 73.377C506.959 75.7285 508.34 77.1436 510.466 77.1436ZM510.466 75.7969C509.386 75.7969 508.688 74.9219 508.688 73.377C508.688 71.8389 509.393 70.957 510.466 70.957C511.546 70.957 512.243 71.8389 512.243 73.377C512.243 74.9219 511.546 75.7969 510.466 75.7969ZM517.329 73.623L517.5 71.0664H519.564V77H521.26V69.7539H516.03L515.798 73.5342C515.729 74.7373 515.497 75.585 514.793 75.585C514.602 75.585 514.458 75.5508 514.39 75.5303V76.9932C514.479 77.0273 514.677 77.0752 514.943 77.0752C516.646 77.0752 517.192 75.7217 517.329 73.623ZM527.542 77H529.237V69.7539H527.542V72.6113H524.479V69.7539H522.784V77H524.479V73.9238H527.542V77ZM533.899 70.9229C534.877 70.9229 535.533 71.6406 535.567 72.666H532.184C532.252 71.6543 532.942 70.9229 533.899 70.9229ZM535.574 74.833C535.39 75.4346 534.788 75.8311 533.995 75.8311C532.881 75.8311 532.17 75.0518 532.17 73.8896V73.7871H537.235V73.2334C537.235 71.0322 535.95 69.6035 533.893 69.6035C531.801 69.6035 530.475 71.1143 530.475 73.4043C530.475 75.7148 531.787 77.1436 533.961 77.1436C535.677 77.1436 536.948 76.2002 537.167 74.833H535.574ZM543.21 77H544.905V69.7539H543.21V72.6113H540.147V69.7539H538.452V77H540.147V73.9238H543.21V77ZM548.104 77L551.256 72.126H551.311V77H552.979V69.7539H551.311L548.159 74.5938H548.104V69.7539H546.437V77H548.104ZM557.634 70.9229C558.611 70.9229 559.268 71.6406 559.302 72.666H555.918C555.986 71.6543 556.677 70.9229 557.634 70.9229ZM559.309 74.833C559.124 75.4346 558.522 75.8311 557.729 75.8311C556.615 75.8311 555.904 75.0518 555.904 73.8896V73.7871H560.97V73.2334C560.97 71.0322 559.685 69.6035 557.627 69.6035C555.535 69.6035 554.209 71.1143 554.209 73.4043C554.209 75.7148 555.521 77.1436 557.695 77.1436C559.411 77.1436 560.683 76.2002 560.901 74.833H559.309ZM569.679 77H571.381V69.7539H564.989V77H566.685V71.0664H569.679V77ZM576.118 77.1436C578.244 77.1436 579.625 75.7285 579.625 73.377C579.625 71.0322 578.23 69.6035 576.118 69.6035C574.006 69.6035 572.611 71.0391 572.611 73.377C572.611 75.7285 573.992 77.1436 576.118 77.1436ZM576.118 75.7969C575.038 75.7969 574.341 74.9219 574.341 73.377C574.341 71.8389 575.045 70.957 576.118 70.957C577.198 70.957 577.896 71.8389 577.896 73.377C577.896 74.9219 577.198 75.7969 576.118 75.7969ZM584.834 69.6309C583.781 69.6309 582.961 70.1641 582.551 71.0049H582.517V69.7539H580.855V79.3857H582.558V75.7969H582.592C582.988 76.6104 583.809 77.1162 584.861 77.1162C586.68 77.1162 587.842 75.6875 587.842 73.377C587.842 71.0596 586.673 69.6309 584.834 69.6309ZM584.314 75.7217C583.255 75.7217 582.551 74.7988 582.544 73.377C582.551 71.9688 583.255 71.0322 584.314 71.0322C585.415 71.0322 586.099 71.9482 586.099 73.377C586.099 74.8125 585.415 75.7217 584.314 75.7217ZM594.521 69.7539H588.375V71.0664H590.604V77H592.292V71.0664H594.521V69.7539ZM599.012 75.7764C597.692 75.7354 596.858 74.8535 596.858 73.377C596.858 71.9072 597.699 71.0117 599.012 70.9775V75.7764ZM602.806 73.377C602.806 74.8467 601.958 75.7422 600.646 75.7764V70.9775C601.965 71.0186 602.806 71.9004 602.806 73.377ZM599.005 77.1025V79.3857H600.659V77.1025C602.983 77.0547 604.521 75.626 604.521 73.377C604.521 71.1074 602.997 69.6992 600.659 69.6514V67.4229H599.005V69.6514C596.667 69.6992 595.143 71.1211 595.143 73.377C595.143 75.6465 596.653 77.0479 599.005 77.1025ZM608.958 77.1436C611.084 77.1436 612.465 75.7285 612.465 73.377C612.465 71.0322 611.07 69.6035 608.958 69.6035C606.846 69.6035 605.451 71.0391 605.451 73.377C605.451 75.7285 606.832 77.1436 608.958 77.1436ZM608.958 75.7969C607.878 75.7969 607.181 74.9219 607.181 73.377C607.181 71.8389 607.885 70.957 608.958 70.957C610.038 70.957 610.735 71.8389 610.735 73.377C610.735 74.9219 610.038 75.7969 608.958 75.7969ZM615.821 73.623L615.992 71.0664H618.057V77H619.752V69.7539H614.522L614.29 73.5342C614.222 74.7373 613.989 75.585 613.285 75.585C613.094 75.585 612.95 75.5508 612.882 75.5303V76.9932C612.971 77.0273 613.169 77.0752 613.436 77.0752C615.138 77.0752 615.685 75.7217 615.821 73.623ZM622.944 77L626.096 72.126H626.15V77H627.818V69.7539H626.15L622.999 74.5938H622.944V69.7539H621.276V77H622.944ZM632.556 77.1436C634.682 77.1436 636.062 75.7285 636.062 73.377C636.062 71.0322 634.668 69.6035 632.556 69.6035C630.443 69.6035 629.049 71.0391 629.049 73.377C629.049 75.7285 630.43 77.1436 632.556 77.1436ZM632.556 75.7969C631.476 75.7969 630.778 74.9219 630.778 73.377C630.778 71.8389 631.482 70.957 632.556 70.957C633.636 70.957 634.333 71.8389 634.333 73.377C634.333 74.9219 633.636 75.7969 632.556 75.7969Z" fill="white"/>
                </svg>

                <div className={s.errorPageTB}>
                    <h1>Аккаунт не верифицирован :(</h1>
                    <p>На вашу почту отправлено письмо с ссылкой на активацию аккаунта.<br/>
                        Если письмо не пришло, проверьте папку «Спам» в вашем почтовом ящике,<br/>
                        либо повторите отправку письма.</p>
                    <NavigateButton link="/">
                        <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path d="M3 12L5 10M5 10L12 3L19 10M5 10V20C5 20.5523 5.44772 21 6 21H9M19 10L21 12M19 10V20C19 20.5523 18.5523 21 18 21H15M9 21C9.55228 21 10 20.5523 10 20V16C10 15.4477 10.4477 15 11 15H13C13.5523 15 14 15.4477 14 16V20C14 20.5523 14.4477 21 15 21M9 21H15" stroke="#111827" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"/>
                        </svg>
                        Главная
                    </NavigateButton>
                    <NavigateButton click={logout}>
                        <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path d="M11 16L7 12M7 12L11 8M7 12L21 12M16 16V17C16 18.6569 14.6569 20 13 20H6C4.34315 20 3 18.6569 3 17V7C3 5.34315 4.34315 4 6 4H13C14.6569 4 16 5.34315 16 7V8" stroke="#111827" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
                        </svg>
                        Выйти из аккаунта
                    </NavigateButton>
                </div>
            </div>
        </div>
    )
}
