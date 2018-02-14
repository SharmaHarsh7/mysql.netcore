ALTER TABLE `Employee` MODIFY COLUMN `Test` int NOT NULL;
ALTER TABLE `Employee` ALTER COLUMN `Test` DROP DEFAULT;
INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20180209094917_emp2', '2.0.1-rtm-125');

