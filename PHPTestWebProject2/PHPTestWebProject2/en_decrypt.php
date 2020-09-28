<?php
function encrypt($plain, $secret, $salt) {
    // Schlüssel für crypt und hmac erzeugen
    $hash = hash('sha256', $secret . $salt);
    echo("<br/>" . 'hash:' . "<br/>" . 'PHP(' . strval(strlen($hash)) . ')' . "<br/>" . $hash);

    $key = mb_substr($hash, 0, 32, '8bit');
    echo("<br/>" . 'key:' . "<br/>" . 'PHP(' . strval(strlen($key)) . ')' . "<br/>" . $key);


    // IV erzeugen
    $method = 'AES-256-CBC';
    $ivSize = openssl_cipher_iv_length($method);
    $iv = openssl_random_pseudo_bytes($ivSize);
    echo("<br/>" . 'iv:' . "<br/>" . 'PHP(' . strval(strlen($iv)) . ')' . "<br/>" . $iv);


    // Verschlüsseln
    $encrypted = openssl_encrypt($plain, $method, $key, OPENSSL_RAW_DATA, $iv);
    echo("<br/>" . 'encrypted:' . "<br/>" . 'PHP(' . strval(strlen($encrypted)) . ')' . "<br/>" . $encrypted);

    $ciphertext = $iv . $encrypted;
    echo("<br/>" . 'ciphertext:' . "<br/>" . 'PHP(' . strval(strlen($ciphertext)) . ')' . "<br/>" . $ciphertext);


    // HMAC erzeugen
    $hmac = hash_hmac('sha256', $ciphertext, $key);
    echo("<br/>" . 'hmac:' . "<br/>" . 'PHP(' . strval(strlen($hmac)) . ')' . "<br/>" . $hmac);


    // zusammenfügen dann zu hex umwandeln und fertig
    $result = bin2hex($hmac . $ciphertext);
    echo("<br/>" . 'result:' . "<br/>" . 'PHP(' . strval(strlen($result)) . ')' . "<br/>" . $result);

    return $result;
}

function decrypt($cipher, $secret, $salt = null)
{
    $cipher = pack("H*", $cipher);
    // Schlüssel für crypt und hmac erzeugen
    $key = mb_substr(hash('sha256', $secret . $salt), 0, 32, '8bit');

    // Split out hmac for comparison
    $macSize = 64;
    $hmac = mb_substr($cipher, 0, $macSize, '8bit');
    $cipherOhneHmac = mb_substr($cipher, $macSize, null, '8bit');

    // hmac neu erzeugen...
    $compareHmac = hash_hmac('sha256', $cipherOhneHmac, $key);
    // ... und mit konstanter Zeit mit übertragenem HMAC vergleichen und abbrechen falls nicht übereinstimmt
    //if (!static::constantEquals($hmac, $compareHmac)) {
    //    return false;
    //}

    $method = 'AES-256-CBC';
    $ivSize = openssl_cipher_iv_length($method);
    // IV extrahieren
    $iv = mb_substr($cipherOhneHmac, 0, $ivSize, '8bit');

    // IV aus verschlüsseltem String entfernen
    $cipherOhneHmacUndIv = mb_substr($cipherOhneHmac, $ivSize, null, '8bit');
    // Entschlüsseln mit dem extrahierten IV
    $decrypted = openssl_decrypt($cipherOhneHmacUndIv, $method, $key, OPENSSL_RAW_DATA, $iv);
    //return bin2hex($decrypted);
    return $decrypted;
}
function decryptold($cipher, $secret, $salt = null)
{
    // Schlüssel für crypt und hmac erzeugen
    $key = mb_substr(hash('sha256', $secret . $salt), 0, 32, '8bit');

    // Split out hmac for comparison
    $macSize = 64;
    $hmac = mb_substr($cipher, 0, $macSize, '8bit');
    $cipherOhneHmac = mb_substr($cipher, $macSize, null, '8bit');

    // hmac neu erzeugen...
    $compareHmac = hash_hmac('sha256', $cipherOhneHmac, $key);
    // ... und mit konstanter Zeit mit übertragenem HMAC vergleichen und abbrechen falls nicht übereinstimmt
    //if (!static::constantEquals($hmac, $compareHmac)) {
    //    return false;
    //}

    $method = 'AES-256-CBC';
    $ivSize = openssl_cipher_iv_length($method);
    // IV extrahieren
    $iv = mb_substr($cipherOhneHmac, 0, $ivSize, '8bit');

    // IV aus verschlüsseltem String entfernen
    $cipherOhneHmacUndIv = mb_substr($cipherOhneHmac, $ivSize, null, '8bit');
    // Entschlüsseln mit dem extrahierten IV
    $decrypted = openssl_decrypt($cipherOhneHmacUndIv, $method, $key, OPENSSL_RAW_DATA, $iv);
    return bin2hex($decrypted);
}



?>
