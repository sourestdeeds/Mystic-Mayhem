This is a script that includes a Vendor (ContractSeller) who sells an Item (MonsterContract).  The Monster Contracts are basically a contract for a random number of a random type of monster.  You go out and kill a monster of the type specified, double click the contract, click the claim corpse button, and it adds to the amount you have killed so far.  After you have killed th required amount of the required monster, you will click the claim reward button and a check in the amount of the reward promised will be placed in your bank.  

This was intended for use by new characters as a way to gain the necessary funds needed to survive.  However, the veteran players will also find this usefull as a quick source of income.  All settings can be adjusted in the scripts and there are a few items to note.

To claim the corpse it must have been killed by the owner of the contract, can not be channeled and all items that you want to keep must be removed from the corpse before claiming.

The MonsterContracts can be used seperate from the ContractSeller if you would like to use them in other scripts, such as quests.

Installation:
Unzip *.cs to your Scripts folder.

Usage:
[ADD ContractSeller
[ADD MonsterContract       { this adds a contract for a random amount, type, and reward }
[ADD MonsterContract( string type, int AmountToKill, int Reward ) { specifies attributes }

Please let me know if you find any bugs, and as always comments, questions, concerns and suggestions are welcome.
