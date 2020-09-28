<?php
echo("<br/>" . 'Start');
include('en_decrypt.php');
$email = 'stammdaten@boservice.de';
echo("<br/>" .'email:' . "<br/>" . 'PHP' . "<br/>" . $email);
//#todo insert your secret here
$secret = 'abcdefghijklmnop';
echo("<br/>" . 'secret:' . "<br/>" . 'PHP' . "<br/>" . $secret);
$salt = '7zTmg5efWD9rQCN8zOWKWVIhXgBbHCeriJM10BQlMenefMcry9';
echo("<br/>" . 'salt:' . "<br/>" . 'PHP' . "<br/>" . $salt);
$unixTimestamp = strval(time());
$plain = $email . '|' . $unixTimestamp;
echo("<br/>" . 'plain:' . "<br/>" . 'PHP(' . strval(strlen($plain)) . ')' . "<br/>" . $plain);


$encrypted = encrypt($plain, $secret, $salt);
echo("<br/>" . 'Ende');
$decrypted = decrypt($encrypted, $secret, $salt);

$boomserverLogin = "https://boom.dev.enapt.de/login?s=";
$completeLink = $boomserverLogin . $encrypted;
echo("<br/>" . $completeLink);






?>

