using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using String_Extensions;

namespace StringClusterScanTests.Parsing;

[TestFixture]
public class TextToWordListTests
{
    [Test]
    public void extract_word_list_from_text()
    {
        // space,score,none * lower-word, title-word, upper-word;
        // = 9 cases; 4 bits with 6 cases spare.
        // +raw, +numbers, +punctuation, +repeat-next

        const string example =
            """
            CPU is repeatedly rebooting on command. 
            WARN:  CPU is repeatedly rebooting from software fault. CPU is repeatedly rebooting from brownout. CPU failed from brownout. System will start in %3d seconds  %3d second%c 


            -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-


            Normal mode #TEST-MODE# #SERIAL-MODE# Unknown programmer mode: '%s' 
            Checking software state... Setting up hardware...
            Reset modem control lines...

            Power up... stable! 
            Ledgers... 
            NVS... bootCount Boot count: %ld



            [36m
            *******************************************
            *                 -Beam-                  *
            * eWater smart network controller for EWC *
            *      Â© 2023 - 2026 eWATER services      *
            ***************************************IEB*[0m

            Firmware version: %ld
            Apr 10 2026 15:39:03 UTC
            Compiled %s

            EWC detected

            Main processes starting 
            Finishing start-up

            All processes started

            #####
            CRITICAL:  
            FAIL:  
            ERROR:  I am eSense on Beam SECMFD_LP_EWC2_5_TAP_SENSE_MT V2.20 02/03/22 SECMFD_LP_EWC2_5_SENSE V2.20 02/03/22 SECMFD_LP_EWC2_5_TAP_MT_V3_PRELOAD V2.23 05/12/24 SECMFD_LP_EWC2_5_TAP_MT_PRELOAD V2.22 19/08/24 SECMFD_LP_EWC2_5_TAP_MT_PRELOAD V2.22 08/03/24 SECMFD_LP_EWC2_5_TAP_MT_PRELOAD V2.21 28/02/24 SECMFD_LP_EWC2_5_TAP_MT V2.20 02/03/22 USER ESEYE1 lastSync FakeDlp outTickLT outTickBC outTickLS outTickGT inTickLT inTickBC inTickLS inTickGT S3IPv4 S2IPv4 S1IPv4 OtaLastPart OtaLastVers OtaNextChunk OtaLastOffs syncOk syncTry SetEwcId AssetName CellNet LastImei EwcSeed LastEwcId LastEwcOta LastGoodDLP CellSimPin CellPassword CellUsername CellApnName ProxEngTimer EngSyncFreq ModemFault VCheckCount LocalSvrLPtr RemoteSvrLPtr RemoteEwcLPtr LocalEwcLPtr AssetId eWaterModem Incoming OTA data failed checksum reading chunk failed (corrupt or truncated?) vers fidx Server reported a zero-sized block (ignoring) BLE client sent unknown formats (%lld). Requested block %d, but got %lld (ignoring) Reported total block count changed from %d to %lld (aborting, may re-try)
            Reported OTA image size changed from %ld to %lld (aborting, may re-try) Server reported a invalid-sized block: %lld (aborting, may re-try) firm Firmware data block ('firm') before a firmware index block ('fidx') Data supplied (%d bytes) was different to declared size (%d bytes) Zero length data block was supplied _err Error in OTA reported by server: '%s' OtaNotFound cx32 BLE msg clear timeout Not in OTA mode Timeout waiting for Beam side BLE ESP32 OTA Copying incoming message failed: received nullptr First OTA block was too small to contain an image header New ESP32 firmware: %s
            Running ESP32 firmware: %s
            Last invalid firmware version: %s
            New version is the same as invalid version. Previously, there was an attempt to launch the firmware with %s version, but it failed. The firmware has been rolled back to the previous version. Current running version is the same as a new. 
            Checking OTA status:  Image pending verify. Mark valid and continue Normal startup No OTA state: original firmware ESP32 firmware updated. Device will reboot! _ok_ ESP32 update FAILED falt Self OTA delayed after reboot Detected invalid OTA version. Rejecting OTA request Versions below %ld not supported. Rejecting OTA request OTA request for current version was made. Will overwrite. OTA request for current version was made. OTA Source not supported: %d. Cannot perform OTA Requested a Self-OTA while one was in progress. Preparing to OTA self to version=(%ld)
            requested a Self-OTA while an EWC OTA was in progress. Configured OTA boot partition at offset 0x%lx, but running from offset 0x%lx. (This can happen if either the OTA boot data or preferred boot image become corrupted somehow.) Partition: %d/%d (offset 0x%lx)
            Trying to recover OTA. Off=%llu; Ver=%llu; Part=%llu; Failed to find recoverable OTA partition. Resetting OTA. Recovering OTA. Could not get a target partition. Is the partition table correct? Writing partition subtype %d at offset 0x%lx
            ; Chunk index is invalid (%d) Modem connection failed Failure requesting OTA block: could not write header imei lily breq No server reply for OTA request No PC-serial reply for Self OTA request Bluetooth lost during OTA Bluetooth timeout during OTA request Problem exchanging messages with Bluetooth client during OTA request BLE msg ready timeout Timeout exchanging messages with Bluetooth client during OTA request (wait ready) fourCCvTargetFree failed to release message!  Received #%d of %d. Complete!
             Completed #%d of %d, next is #%d (%d %%)
            ESP32 image chunk error (%d)
            Server protocol unknown esp_ota_begin failed (0x%x: %s)
            OTA Start
            Starting OTA recovery. Chunk=%d; Offset=%lu; Writing OTA data to flash failed (%x) Server declared %ld bytes, received %ld. OTA Rejected 

            Preparing to write OTA to flash.
            Transferred %d network bytes to read %d image bytes.

            Image validation failed esp_ota_end failed (%s)! esp_ota_set_boot_partition failed (%s)! Self OTA Failed during finalise. May retry later 
            OTA complete.
            System restart!

            Resetting state; reason=%d
            error setting advertisement data; rc=%d (Z)
            error enabling advertisement; rc=%d

            Connection failed. Restarting advertise
            Could not connect to bluetooth (%d) Unexpected subscription handle: %d; expected: %d;
            Unhandled event type: %d; Error notifying BLE client; rc=%d
            Error creating mbuf from data Connection lost?
            Connection lost, or invalid MTU
            Failed to send block in MTU split BLE Host Task Started

            BLE Host task stopped.  Unexpected op in gatt_svr_register_cb
            error setting up address; rc=%d
            error determining address type; rc=%d
            os_mbuf_append failed: %d; 
            GATT buffer out of characteristic size limits
             E!(gatt_svr_chr_write:2)  
            Received hard termination message. Rebooting now!
            Failed to send message to Android
            DEAD Error in gatt_svr_chr_access (op=%d) 
            Stopping BLE and notification task 
            Previous status is not ok: %d; nimble_port_init failed?: %d; gatt_svr_init failed?: %d; Setting device name to '%s' Could not set device name 
            Could not set bluetooth device name

            Starting bleprph_host_task... Lost connection before sending message 
            Rejecting send status: key is too large 
            Rejecting send status: value is too large 
            Failed to create fourCCv target for BLE message
            Failed to write version stat Failed to write status block Failed to write CRC32 
            Failed to send status to bluetooth
            logs 
            Failed to send log to bluetooth
            Starting BLE... BLE is up
            Stopping BLE... BLE is down
            xKVP: %p; 
            CRC failed. Could not read value

            CRC failed. Expected 0x%lx, got 0x%lx
            Invalid FourCCv from byte feed Received message Byte feed exceeded container size Sync task is already running syncTask Could not start sync task EWC task is already running ewcDataReader Could not start ewcDataReader task Modbus reader task is already running modbusReader Could not start Modbus reader task RS232 detection task is already running rs232Detect Could not start rs232Detection task Sleep monitor task is already running sleepMonitor Could not start sleepMonitor task SD Card task is already running ledgerSync Could not start ledgerSync task Bluetooth protocol task is already running bluetoothComms Could not start bluetooth protocol task EWC serial adaptor  task is already running ewcSerialAdp Could not start EWC serial adaptor task PC test mode task is already running testMode Could not start PC test mode task Ran out of command buffer space. App is too fast!  WAIT MTU Seg Lost (start, start) 
            MTU failure at start (protocol failure)
            Buffering message failed (1) 
            Could not create a new partial cursor
            MTU Seg Lost (mid) 
            MTU failure mid message (protocol failure)
            MTU Seg Lost (end) 
            MTU failure at end (protocol failure)
            MTU Seg Lost (start, complete) 
            MTU failure at start/complete (protocol failure)
            MTU Seg Lost (start, NS) 
            MTU failure at start/NS (protocol failure)
            Invalid Bluetooth data segment Bluetooth task awake. CTS %2d ms; ENG %3d; Failed to send eng status ENG BT:ok; BT:fault; BT:dis; BT:off; OTA-LOCK %3d; Dropped server ledger item as it seems invalid (%lld) Duplicate server item (new=%lld, tide=%lld). %d left, %d free.
            Tried to drop duplicate server ledger item, but it was empty? Command with ledger ID %lld uses %d bytes, which exceeds buffer limit %d. Will be skipped. Ledger is not working (fails=%d) Failed to read server command with ledger id=%lld into buffer. Will try again next cycle Back-fill done DLP %d == %d Failed to update last good DLP. %d is behind old value %d Tag out of credit ON EWC-PWR Engineer mode on tamper change Failed to insert into EWC ledger DLP in sync
            Event: back-fill %d to %d

            DLP backwards. Reset to %d from %d
            Unexpected: looks like a command reply, but wasn't expecting one Unknown event with no datalog pointer. Cmd: back-fill %d to %d

            DLP backwards in cmd. Reset to %d from %d
            Unexpected reply to DLP request (should be %d, but got %d) Got a back-fill for DLP %d, but was not in back-fill mode Got a back-fill for DLP %d, but should have got %d Back-fill done at %d  Back-fill out of sequence: expected %d, got %d  Failed to remove replied-to EWC command CTS signal active while processing datalog Unexpected state in `processDatagram`: %d. Will reset to wait-event Could not generate EWC open-valve command Could not generate EWC close-valve command Fixed serverCommandIsWaiting Fixed topUpExchangeIsWaiting EWC command too long Failed to queue command in ewc_queueEwcHexCommand Invalid EWC command requested (no CTS) Timeout while writing to EWC (no CTS wait) 
             To EWC: %s; Timeout waiting for access to EWC (no CTS wait) Invalid EWC command requested EWC is not ready to accept commands Timeout while writing to EWC Lost connection to EWC. Rebooting! Reporting EWC fault and requesting sync. Timeout waiting for access to EWC Could send read-EEPROM command to EWC EWC did not reply to read-EEPROM command EWC did not give expected reply to command EWC reply had incorrect address EWC Disconnected? Could send write-EEPROM command to EWC EWC did not reply to write-EEPROM command Invalid credit region supplied Skipping valve check: EWC is disbursing again Could not generate EWC status check command  Waiting for CTS  Could send status command to EWC EWC did not reply to status command EWC did not give expected reply to status command Skipping valve check: tag is present Skipping valve check: sample time is too short (%d) Valve check passed (%ld ticks detected) Detected closed-valve flow. Time=%d; Ticks=%ld; Could send version command to EWC EWC did not reply to version command EWC='%s'
            Known EWC firmware, OTA ID=%d; CTS wake=%lld.
            Unknown EWC version, no last OTA ID. OTA should trigger soon. Unknown EWC version, OK last OTA ID=%d
            Could send Get Time command to EWC EWC did not reply to Get Time command EWC clock has reset flag set EWC clock is invalid Unacceptable date '%s' Set ESP32 clock
            Last EWC ID is not valid. Not writing (0x%lx) WRITE EWC ID, try %d of %d
            Read EWC ID, try %d of %d
            EWC ID= 0x%08lx
            No credit region set. Writing credit region Credit region set Starting compare/exchange Failed to read Supertap slot %d Triggered cmp-exc without valid data Cmp-ex non-match. Writing failure to NVS Advancing server ledger Cmp-ex match. Writing success to NVS Could not write to slot %d Could not read from cmd buffer at %lld Top-up exchange is waiting for slot=%d. Could not read top-up exchange command at %lld Skip entry in ledger at %lld Unknown ledger entry type %d, at %lld. Dropping 
            Switched to EWC Baud 
            RUNNING IN SERIAL ADAPTOR MODE. ALL LOGGING WILL BE DISABLED! Requesting start-up initial sync

            EWC Jump %lld ms  Cycle=%4lldms->%c; +D Delaying EWC update, modbus is active. VCT=%d Delaying valve check, modbus is active. Valve check... Fault CTS skip DLP for pre-sync...
            Fill[%d...] EWC ledger over half: Do sync
            EWC did not update Failed to write command to EWC sending direct command Failed to read queued direct command Failed to write direct 'hex' command to EWC Unexpected cState during CTS active: %d CTS-OK Ledger very full. Requesting immediate sync.
            Datagram length exceeded buffer! (%d of %d) Unexpected reply to last data (%d) EWC did not reply to most recent command (%d) EWC is not responding to a command. Will be dropped. Unexpected state at end of CTS cycle: %d. Resetting to EC_WAIT_FOR_CTS Ledger insertions seem to be failing continuously. Will reboot. Commands still waiting at end of loop: svr=%d; dir=%d; cmp=%d; Skipping Modbus read due to other processes. 
            Reading EWC message. Unexpected non-command message from server (server ledger id= %lld). Dropping Unexpected EWC command ID: %d Failed to read waiting command from server ledger. Skipping. Found EWC message (at %lld) Tag-top-up cmp-ex command sent to a sense-only device, (at %lld). Dropping Reading modbus devices for sync. cdev closed open Doing initial device scan Tamper switch changed. Triggering ENG mode. Tamper=%s;
            We have %d EWC replies. Should send these to PC. Failed to write version for EWC events Failure to write this ledger to outgoing buffer! Ledger failure! Could not peek EWC event. Wrote EWC message (dlp=%d) Failed to write CRC32 for EWC events 
            Sending EWC message data (%d bytes)
            Target is invalid Buffer failed to allocate Failed to find complete message after %d bytes. 
            @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            Modbus RS485 any 
            Starting RS232 detection Device scan paused: other processes active. 
            Scanning for %s devices (%d of %d)... Detected EWC during start-up. Starting EWC task. Looking for EWC bootloader Looking for EWC CTS strobe Found EWC in bootloader mode. Starting EWC task. Saw EWC CTS strobe. Starting EWC task. 
            No EWC found 
            Looking for Modbus network 
            Looking for Modbus network on RS232 port Found Modbus network on RS232 (Switch to eSense mode) 
            Looking for Modbus network on break-out port Found Modbus network on break-out. (Switch to Modbus+EWC mode) No MODBUS devices found Failed to find any connected devices 
            Device tasks running.
            Ending device scan. 
            No devices found. Delaying before restart  (?)   (i)  -> Failed to set CTS wake Failed to set wakeup timer Aborting sleep Clock jump? Time is %s ? Clock jumped? Time is set to %s Up=%4dms, Zz=%4dms; 
            ST %3d, %s; SYNC Last restart reason: Core0= %s; Core1= %s;
            Self OTA failure reported EWC OTA failure reported Ledger Fault 
            Server retracted commands up to Sptr=%lld. Failed to read 'lost' block NVS Fault Stuck valve fault Flash memory fault Sync process timed out Sync process timed twice SYNC process exceeded hard limit multiple times. Will reboot! 
            Sync flags clear.
            Critical errors reported:  
            Sync fault flags [%c%c%c%c%c%c%c%c%c%c%c%c%c%c%c%c%c]
            Could not allocate memory for server reply Incoming data did not pass checksum Failed to read item. Block damaged or truncated tptr Failed to read server confirmation of EWC-side ledger Server gave an invalid confirmed pointer (%lld). Will ignore. Server gave us a confirmed pointer lower than before (%lld vs %lld). Possible fault on server side. Will write 'lost' messages. Resetting last confirmed EwcPtr to %lld Server gave a confirmed pointer (%lld) ahead of our high tide. Might be NVS reset on ESP32. EWC ledger last confirmed pointer moved from %lld to %lld.
            EWC ledger high tide pointer moved from %lld to %lld.
            sptr Failed to read server-side ledger high-tide Server gave us a zero-tide! (new=%lld; last=%lld) Server gave us a high-tide lower than before (%lld vs %lld). Will reset. Failed to read time block Failed to read desired firmware version Invalid firmware version %lld. Ignoring Target ESP32 firmware: %lld. Current: %ld
            ewcf Ignoring server version request. OTA already in progress. Target EWC firmware: %lld. Last known: %ld
            Server rejected sync message: '%s' cmds Failed to read 'cmds' block mdmc Ignoring modem AT command
            rcon Ignoring remote control command
            bind Failed to read new asset assignment Switched to asset %lld
            lset Failed to read 'lset' block. Ignoring Unknown setting in 'lset' block (%s). Ignoring tuxc Failed to read 'tuxc' block Unexpected length of cmp-ex message: %d lost Sync reply complete
            Request to advance server ledger Removed invalid server ledger entry Server side ledger out of sync. Expected to confirm an item, but the ledger was already empty! Sptr to %lld
            Local generated command processed New sptr %lld is behind highest (%lld). Will not move ptr 
            Ledger empty. Will sync soon to reply.
            Last server '%s'
            CtsBad EwcNoCmd NoEwc NetDrop BadReply BadNVS BadRam StuckValve BadFlash BadUART NoLedger ClkJmp NoSleep BadCmd MdmLock NoReply BadSptr BadPwr Logic WDT EwcBoot netw ota_ BadSelfOta ewc BadEwcOta Writing to sync buffer failed during high-priority items sabc Did not write modbus data to sync. Writing to sync buffer failed after basic items Failed to read cmp-ex slot %d Mismatch in cmp-ex. Expected=%d; Actual=%d; Ran out of space writing cmp-ex slots (size=%d) Tracking pointer value is invalid! (%llu/%lld) Requested data lost=%llu (%lld)
            Ran out of space for ledger entries Waiting EWC messages = %d Could not peek into EWC ledger (idx=%d) Entry in server ledger is invalid (%lld) Will skip Ran out of space for ledger 'lost' entries mdmi envy sim_ Sync buffer written (ok = %d, crc-ok = %d) length = %d
            Sync timer is passed the failure threshold (%d > %d). Rebooting ESP32 CTS*2 CTS/2  ST=%d? (missed %d CTS)  Failed to setup sync backstop timer Failed to start sync backstop timer SHTi 
            Received server push message
            Push message Failed to interpret server push message Power up for sync...
            Trying server at %s...

            Sync attempt %d of %d
            Time: %s
            Rejecting sync, as an OTA is in progress Failed to start modem. Write to buffer failed
            Failed to send sync message Problem exchanging messages with server.
            Could not process server reply Failed to remove cmp-ex at slot %d A critical fault was reported. ESP32 will reboot. Ignoring pending ESP32 OTA due to time-of day OTA locked out due to errors (%d) Trying to OTA ESP32 from %ld to %ld
            Ignoring pending EWC OTA due to time-of day Ignoring server OTA as Bluetooth client is connected. Trying to OTA EWC from %ld to %ld
            Sync done with OTA report pending Sync done (eng mode) Cycling modem for trace Sync failed (eng mode) Sync done (CTS %d). New data for EWC to process. Sync done (CTS %d). New server commands for Modbus task. Sync done with more work to come, but EWC signals not seen. <SYNC complete> <SYNC failed> <Starting SYNC> MLogNote 
            Sync took %lld s
            NoSync(locked %d) OFF NoSync(pause %d) 

            Time to sync (Eng=%d; freq=%d; timer=%d; pss=%d)
            START 
            ***************************************


                 Sync SUCCESS (%llu of %llu)  


            ***************************************
            %d previous failures

            GOOD 
            ### Sync FAILURE (%d)###
            Sync is not working. Restarting ESP32 Sync is not working. Restarting modem Ledger still populated. Will sync again.
            OTA status waiting, will trigger another sync %s - %d bytes, buffer is NULL
            %s - %d bytes from %p:
            %02x      Ã— Â· dequeueAddLastRange: Total size over element size (size=%d, elements=%d, prefix=%d, total=%d) dequeueAddLastRange: Queue is full dst looks invalid! %p
            ledgerInitialise() called twice! ########## CRITICAL FAULT ##########
            Could not allocate main buffers for sync ledgers! ########## CRITICAL FAULT ##########
            Could not allocate data buffers for sync ledgers! EWC event ledger out of space Copy data to EWC circular buffer failed (%d of %d) Total fail in ledger_EwcEvents_Insert dequeueAddLast failed! EWC ledger entry written. Slots: %d used, %d free; Bytes: %d used, %d free
            Could not store EWC event: ledgers not initialised Lock timed out in ledger_EwcEvents_Insert Could not pop EWC event: ledgers not initialised Lock timed out in ledger_EwcEvents_Pop Could not size-peek EWC event: ledgers not initialised Lock timed out in ledger_EwcEvents_Peek ledger_EwcEvents_CopyIntoFourCC was given an invalid index (%d) ledger_EwcEvents_CopyIntoFourCC was given an index greater than available capacity (%d) ledger_EwcEvents_CopyIntoFourCC: Invalid entry data length (%d) Invalid datalog length in EWC ledger index (size=%d) EWC Ledger Index points out of stored data Unexpected long headers! Failed to read EWC ledger data into buffer Could not copy EWC event: ledgers not initialised Lock timed out in ledger_EwcEvents_CopyIntoFourCC Lock timed out in ledgerGetWaitingLength_Ewc Lock timed out in ledgerGetFreeLength_Ewc Invalid data supplied to ledger_ServerEvents_Insert Remaining capacity too low in ServerLedgerData (have %d, need %d) Remaining capacity too low in ServerLedgerIndex (have %d, need 1) Copy data to server circular buffer failed (%d of %d) Total fail in ledger_ServerEvents_Insert Copy index to server circular buffer failed (%d of %d) Server ledger entry written. Slots: %d used, %d free; Bytes: %d used, %d free
            Could not store Server event: ledgers not initialised Lock timed out in ledger_ServerEvents_Insert Could not size-peek Server event: ledgers not initialised Lock timed out in ledger_ServerEvents_Peek Failure removing item from non-empty server ledger Could not pop Server event: ledgers not initialised Lock timed out in ledger_ServerEvents_Pop ledger_ServerEvents_CopyIntoBuffer was given an invalid index (%d) ledger_ServerEvents_CopyIntoBuffer was given an index greater than available capacity (%d) ledger_ServerEvents_CopyIntoBuffer: Invalid entry data length (%d) Server Ledger Index points out of stored data Failed to read Server ledger data into buffer Could not copy server event: ledgers not initialised Lock timed out in ledger_ServerEvents_CopyIntoBuffer Lock timed out in ledgerGetWaitingLength_Server Lock timed out in ledgerGetFreeLength_Server strlen failed: out of safety limit. (%s);  Rotbuf allocations: buffers=%d, cursors=%d; 

            Failed to create buffer mutex!

            Invalid cursor: bad start;  Invalid cursor: bad length;  Invalid cursor: alloc failed;  Cursors attached to reset rot-buf Rotbuf content after push Rotbuf content after consume Invalid sleep state  Sleep:ok;   Sleep:%c%c%c%c%c%c%c%c%c%c%c; Failed to stop UART driver on port %d UART config failed (%d) UART port inversion setup failed (%d) UART pin setup failed (%d) UART driver install failed (err=%d; hw port=%d; physPort=%d) Tried to create UART device on occupied port %d. Failed to allocate space for UART device on port %d. Failed to connect UART device to port %d. Sleep not locked when waiting for port %d. UART: Invalid held port! Tried to back-off a UART device from the wrong port (Current=%d, Requested=%d);  UART lease already expired when back-off requested (Leases=%d, current=%d, released=%d);  Tried to release a UART device from the wrong port (Current=%d, Released=%d);  UART lease already expired when released (Leases=%d, current=%d, released=%d);  Over-release of port (%d) Tried to send to invalid device Tried to send data to the wrong port (expected=%d, actual=%d); uartSendString should have sent %d, but sent %d error while waiting for UART send: %d Tried to read data from the wrong port (expected=%d, actual=%d); Tried to wait for data on the wrong port (expected=%d, actual=%d); Tried to check data buffer on the wrong port (expected=%d, actual=%d); Failed to read UART buffer length (err %d, len=%d)  Tried to drain data on the wrong port (expected=%d, actual=%d); Failed to set wake-on-data: %d Lock timed out in serialOutRaw Lock timed out in internalLog TASK ENDED! Time-out waiting for lock. Self=%p; Holder=%p; Failed to gain access to serial port for serialReadRaw storage Error (0x%x: %s) opening NVS handle! Error (0x%x: %s) committing NVS changes! 
            Starting engineer mode burst

            Starting engineer mode
            Pwr:%c%c%c%c%c; nvs Partition %s found, %lu bytes Partition %s not found! 
            Dump page %d 
            State is %08lX 
            Seqnr is %08lX 
            CRC   is %08lX
            Key %03d: %s Failed to open NVS Error (0x%x) reading NVS Data stored for key '%s' exceeds the space allocated for it (%d) Error (0x%x: %s) reading NVS (nvsReadWithDefault_string) Invalid buffer size in nvsRead_array Error (0x%x: %s) reading NVS (nvsRead_array) Error (0x%x: %s) writing to NVS NVS partition is truncated and needs to be erased Unexpected fault clearing NVS flash (0x%x: %s) NVS flash was erased NVS partition has never been initialised. This may be a flash-write issue Error (0x%x: %s) Failed to configure NVS flash 
            NVS stats: free=%d; used=%d; total=%d; n-spaces=%d.
            Could not read NVS stats (0x%x: %s) TEST NVS ready
            CmpExFlags CmpEx_? Tried to remove empty slot %d Flags before removal = %llx Flags after removal = %llx Could not read compare-exchange flags from NVS Invalid cmp-ex slot requested: %d Failed to read cmp-ex slot (%s) %04d-%02d-%02d %02d:%02d:%02d Ignoring power down request as a Bluetooth OTA might be happening RESTARTING Hard reset due to fault! Restarting on request Lock time out in auxSetPowerConnection 
            Stopping engineer mode
            Fault when trying to set wakeup condition on pin %d: %d (%s) POWERON_RESET SW_RESET OWDT_RESET DEEPSLEEP_RESET SDIO_RESET TG0WDT_SYS_RESET TG1WDT_SYS_RESET RTCWDT_SYS_RESET INTRUSION_RESET TGWDT_CPU_RESET SW_CPU_RESET RTCWDT_CPU_RESET EXT_CPU_RESET RTCWDT_BROWN_OUT_RESET RTCWDT_RTC_RESET NO_MEAN free=%zuk min=%zuk all=%dk; Free memory low! Free memory exhausted. Rebooting!  WiFi /  BT  BLE embedded external esp32 %s, %d cores,%s%s%s, CPU %luHz,  Could not read CPU clock setting. rev %d.%d, Get flash size failed! %luMB %s flash
            Flash size is invalid! Beam requires 4MiB minimum. Check correct chip and partition image! Min free heap: %lu bytes of %lu
            Modem waits were cancelled. Modem waits enabled. MLogOut MLogIn Failed to send modem trace Modem <time-out, %d bytes from modem>
            <time-out> <time-out, %d bytes from modem> <time-out, msg-err> Modem <failure: %s>
            <failure> Modem: %s

            Modem fault: %s
            Modem <received %d bytes>
            <received %d bytes> Failed to generate trace message (traceDataReceived)
            Modem <%s: %s>
            <%s: %s> Failed to generate trace message (traceStatus)
            Modem <sent %d bytes to %s>
            <sent %d bytes to %s> Failed to generate trace message (traceDataSent)
            Command timeout: '%s' after %d ms ERROR Did not get complete reply to command: '%s' after %d ms Buffer at failure Failed to generate command (sendCommandFs)
            'waitForMessage' exceeded safety limit Did not see message '%s'
            Failed to generate command (sendCommandFormatAndWaitForTerminator)
            AT+CSQ Failed to read signal quality CSQ: Could not interpret signal quality reply <RST> <RST/PWR> Modem off cmd...
            POWERED DOWN AT+QPOWD 
            Modem off
            No 'off' response! Will reset by hardware.
            ATI ** Failed to connect to the modem! ** Quectel 
            Quectel modem OK.
            INCORPORATED 
            SIMCOM modem! Wrong Firmware!
            MeiG 
            MeiG modem! Wrong Firmware!

            Unknown modem!
            AT+CPIN? Failed to read SIM card details READY SIM Ready: No PIN required CME ERROR: 10 SIM not inserted CME ERROR: 14 SIM not ready yet CME ERROR: 15 SIM not compatible with this modem SIM likely needs a PIN or PUK code: '%s' AT+ICCID ICCID: 
            ICCID: '%s'
            AT+CGSN AT+CGSN

            OK 
            IMEI: '%s'
            AT+CRSM=176,28539,0,0,12
            Could not read SIM access data AT+CRSM=214,28539,0,0,12,
            Could not clear SIM access data SIM forbidden list cleared. AT+CEER Unknown Last error (read after restart): '%s'
            Could not read error code from modem. Modem serial off Modem wake-up
            Wait for modem up...
            RDY Modem did not reply ready after 30 seconds ATE1
            Checking module details Module type check failed. Cannot start modem. Scanning network. Please wait. AT+COPS=? AT+CIMI Tracing modem calls without scanning network Could not read SIM card. Try removing and reinserting. PIN Required by SIM card, but no PIN is configured! Supplying PIN
            AT+CPIN=%s SIM refused the PIN that is configured Could not read ICCID Failed to reset forbidden list 
            ERROR
            Timeout waiting for reply AT+CREG? AT+CGREG? AT+CEREG? Failed to read current operator registration REG: %s gave invalid response: '%s' Unexpected C*REG reply: %d (%c in %s) AT Starting network attach AT+COPS=0 Failed to switch to automatic network selection AT+CGATT=1 Failed to attach to network Modem stopped responding after attach request. Will attempt reset Network attach OK %s Network registration failed %s Unknown network registration DENIED denied SIM Network connection denied. Check SIM activation %s Network offline and not searching %s Network searching %s Unknown network registration status %s Network ready, on home net %s Network ready, roaming %s Unknown status (%d) Checking network 4G/LTE 3G 2G All networks offline and not searching. Network attachment failed AT+QICSGP=1,1,"%s","%s","%s" AT+QICSGP=1,1,"%s" 
            Sending APN '%s' Failed to set APN name, may fail later No APN is configured AT+CFUN=1,0 AT+QCFG="roamservice",2,1 AT+QCFG="nwscanmode",0,1 
            Registration failed
            Failed to connect to network AT+QIACT=1 Failed to activate PDP context
            . AT+QIDEACT=1 Failed to DE-activate PDP context AT+COPS? +COPS:  Attached operator Network provider changed Failed to update network provider Same network provider: %s AT+CGPADDR=1 +CGPADDR: 1, Local IP address Turning off modem data session AT+QICLOSE=1 Failed to close network Waiting for reply (up to %d ms) "pdpdeact",1 "recv",1, Disconnected from network ('%s') Timeout waiting for server to reply (Server '%s', %d ms). Reply buffer after time-out +QIURC:  Waiting for more data Modem module did not deliver received header Reply buffer after failure Reply buffer after clean-up Could not read data length from modem reply. Read invalid data length from modem reply. Did not copy expected data (%d declared, %d copied, %d available) Server reply received Reply buffer after message received Failed to read all data from modem Cannot start modem Could not read IMEI Turning off modem connection. Failed to soft-close data session. Will power off soon. Modem power down. Modem is off? AT+QINDCFG="all",1,1 Failed to configure modem status messages AT+CGATT? Could not read connection state. Powering modem off. CGATT: 0 Data connection has failed. Powering modem off. Data is up. Starting data session... Setting attaching to network provider failed. Cannot sync. NO VALID REMOTE SERVER SELECTED Opening UDP session, target server '%s' AT+QIOPEN=1,1,"UDP","%s",420,0,1 Failed to open UDP session. Cannot communicate! +QIOPEN: Modem did not reply with QIOPEN message Modem replied with error on QIOPEN Could not interpret QIOPEN message Opened UDP session. AT+CMEE=2 AT+QICFG="transpktsize",1400 AT+QICFG="udp/readmode",1 AT+QICFG="recvind",1 AT+QICFG="viewmode",1 AT+QICFG="pdp/retry",1,1,4,8 AT+QICFG="pdp/retry",1,0,4,8 Data up OK
            Open UDP failed with code %s Lock timed out in modemEnableData. Possible logic error Data session was down. Restarting... Failed to enable data. Network is not available. modemSendUdp: buffer was null modemSendUdp: input length was invalid (%d) Trying to send %d bytes to remote server
            AT+QISEND=1,%d Modem not ready Failed to send data to modem UART UDP Sent data SEND OK
            SEND FAIL Exceeded modem send buffer Modem did not give 'SEND OK' message Modem died during send UDP sent. Modem connection release (%d). Periodic modem shutdown and clean-up. Leaving modem up (%d of %d) 
            Starting trace
            <Modem trace on> 
            Starting trace from toggle. Search = %d
            <Modem trace off> Modem not responding. Will reset Failed to enable data. Mdm:off Mdm:ok Modem down unexpectedly. New data: %d bytes
            Last comms with modem Modem down on schedule
            Sending broadcast message Bad Modbus address: %d (0x%x) Bad Modbus register ID: %d (0x%x) Bad Modbus register count: %d (0x%x) Sending broadcast WRITE message Invalid buffers in modbus_parseReply Empty message in modbus_parseReply Modbus device replied with error code 0x%x. Truncated message? Bad CRC? Bad function? Bad register? (unknown code) 0xFE detected. Skipping. Invalid reply length in modbus_parseReply (%d) Modbus data CRC failure in modbus_parseReply CRC failure in modbus_parseReply - Modbus data received Error reply from device: DeviceAddr=%d; Code=%d; Command echo Length mismatch in modbus_parseReply (expected %d, got %d) Failed to gain access to UART Failed to send Modbus to UART Modbus data exceeded buffer No reply Bad reply Invalid Modbus port Invalid modbus function: %d
            49.12.234.39 %d.%d.%d.%d 116.203.74.105 162.55.54.252 Tried to commit invalid server ledger pointer (%lld). Ignored. Sync frequency out of normal range (%d). Will reset to 400 Engineer mode sync frequency out of normal range (%d). Will reset to %d EWC firmware out of normal range (%ld). Will reset to %d Updated EWC firmware NVS = %ld Invalid IMEI, not storing Oversize BLE name. Truncating to '%s' Asset name is larger than the limit (%d). Truncated to '%s' Cell network provider name is larger than the limit (%d). Truncated to '%s' Reset server ledger last stored pointer to %lld Server command to set sync frequency was out of safe bounds. Ignoring Changed sync frequency to %d Server command to set engineer-mode sync frequency was out of safe bounds. Ignoring Changed engineer-mode sync frequency to %d Changed valve check delay to %d Remote side sync value is invalid: %lld. Ignoring. Server Ledger id=%lld Server reset: EWC ledger last confirmed pointer moved from %lld to %lld. Server Ledger id=%lld. Server command to set confirmed datalog pointer was out of safe bounds. Ignoring Changed last 'good' DLP to %lld Changed EWC firmware ID to %ld Reset sync statistics Changed PROX timer to %d RESET_ALL Cell network APN provided is larger than the limit (%d). Truncated to '%s' Could not send bootloader info command to EWC No bootloader response EWC in bootloader mode
            Saw data from EWC, but was not bootloader reply Did not find EWC bootloader EWC firmware updated EWC update FAILED EWC-OTA EWC-OTA: Unexpected final status target: %d EWC OTA status sent 
            FAILED TO SEND EWC OTA STATUS
            prog EWC-OTA: Unexpected progress status target: %d Switch EWC to program mode... Could not generate request-to-program command Could send request-to-program command to EWC EWC did not reply to request-to-program command EWC refused to switch to programming mode (error reply) Prog mode ready EWC refused to switch to programming mode (status flag reply = %x) Unexpected response from EWC Could not detect EWC CTS strobe %d consecutive bootloader errors. Aborting OTA. End of firmware image. EWC should be reset.
            Command line will over-run image data at %ld (%ld of %ld) Last block written. EWC should be reset.
            EWC did not reply to command (%d) OTA %lu..%lu of %lu


            Processing firmware image complete

            Using built-in recovery image BLE msg clear timeout (%lld ms) BLE msg ready timeout (%lld ms) Timeout waiting for Beam side BLE EWC OTA Loading BLE message... Loaded. Reading IMEI Failure requesting OTA block: could not write fourCCv values Problem exchanging messages with server during OTA request No PC-serial reply for EWC OTA request Lost connection to Bluetooth client during OTA Timeout exchanging messages with Bluetooth client during OTA request (wait clear) Protocol error EWC OTA image size is out of acceptable range %d > %ld > %d Could not allocate buffer for EWC OTA image EWC OTA: Retry limit exceeded during OTA. Aborting update EWC OTA: Server protocol did not match one we understand EWC OTA: Server does not have specified version EWC OTA: OTA block was corrupted in transit. Try %d of %d EWC OTA: Failed to read OTA block. Try %d of %d EWC OTA: OTA data received (%d) over-ran the size declared (%ld) EWC OTA: Tried to copy to backup image (logic error) EWC OTA: Loaded bytes: %ld
            EWC OTA: Total Write binary data length: %ld
            EWC OTA: Failed to read OTA image: never got first data EWC OTA: Server declared %ld bytes, but I received %ld. Will reject this OTA Failed to read OTA image Expected a minimum image size of %u bytes, but I received %ld. Will reject this OTA OTA write complete Could not access UART Could not switch EWC to programming mode requested an EWC OTA while a Self-OTA was in progress. Not enough free RAM to perform an EWC OTA Loading EWC OTA from recovery data. No buffer for data EWC image loaded. Writing...
            EWC OTA Clean-up...
            EWC OTA update complete
            EWC OTA Failed EWC connection seems broken. Rejecting OTA request EWC OTA delayed after reboot Detected invalid OTA version. Rejecting OTA request (Timer=%d) EWC OTA request for current version was made. Will overwrite EWC OTA request for current version was made. Rejecting EWC ID is stored as %lu, this might be invalid, which will cause the OTA to fail OTA Source not supported. Cannot perform OTA (src=%d) requested a EWC OTA while one was in progress. Preparing to OTA attached EWC to version %ld
            Waiting for any sync to end... Starting OTA... EWC appears to be in bootloader mode. Will try to trigger recover it Failed to recover EWC offline. Will flag for OTA NVS prev=%ld; Failed to read decimal places from XG-136 meter XG136 meter - Unexpected multiplier: %d Failed to read units of measure from XG-136 meter Unexpected unit-of-measure (%d) on XG-136 meter 
            XG136 Depth: %fm; units=%d; dp=%d; Failed to read decimal places from CWT-WLS-S meter CWT-WLS-S meter - Unexpected multiplier: %d Failed to read units of measure from CWT-WLS-S meter Unexpected unit-of-measure (%d) on CWT-WLS-S meter 
            CWT-WLS-S Depth: %fm; units=%d; dp=%d; 
            NT18B07 Temp: %f degC; 
            N4AIA04 V1: %fV; V2: %fV; C1: %fmA; C2: %fmA; 
            TMPC04 Pulse count: %ld; Got zero reading from MPC04 pulse counter. Waiting for more ticks. Rebooted since last tick reading. May have lost ticks Pulse counter reset before reaching roll-over threshold. Ignoring ticks (this count=%ld, prev count=%lld) Previous pulse count is invalid. Ignoring ticks (%lld) Pulse counter rolled over since last reading Failed to read TMPC04 pulse counter (in-flow) In-flow tick gain is over safety limit. Not adding to total (reading: %lld, limit: %lld) Read tick counter in-flow (count=%ld, gain-this-cycle=%lld, grand-total=%lld, waiting-to-sync=%lld, ok=%d) Failed to read TMPC04 pulse counter (out-flow) Out-flow tick gain is over safety limit. Not adding to total (reading: %lld, limit: %lld) Read tick counter out-flow (count=%ld, gain-this-cycle=%lld, grand-total=%lld, waiting-to-sync=%lld, ok=%d) 
               found temp sensor Found device: Temp sensor NT18B07 Modbus 
               found volt sensor Found device: Volt/Current Sensor N4AIA04 
               found pulse counter (inflow) Found device: Pulse Sensor tMPC04 (inflow) 
               found pulse counter (outflow) Found device: Pulse Sensor tMPC04 (outflow) 
               found XG136 depth sensor Found device: Depth Probe XG136 (5m) 
               found CWT-WLS depth sensor Found device: Depth Probe CWT-WLS (5m) %.3e rsen Scanning RS485 port Timeout waiting for access to RS485 port 
            Reading inflow accumulator

            Reading outflow accumulator
            Last sync in-flow tick accum = %lld; No in-flow ticks in last sync cycle. Last sync out-flow tick accum = %lld; No out-flow ticks in last sync cycle. Failed to insert synth StatusReply into EWC ledger Failed to insert synth TickAccum reply into EWC ledger Failed to insert synth HealthCheckEvent event into EWC ledger Failed to insert synth DispenseLimit event into EWC ledger Sent %lld ticks over %lld seconds as EWC event. Skipping EWC flow event due to low flow (%lld ticks, %d skipped cycles). Not outputting sensor readings or events, as we don't have a valid clock yet. Not outputting sensor readings or events, as the last readings were too recent (%lld s) Writing Modbus values (%lld seconds since last reading)     Read XG-136 depth sensor. Ok=%d, depth=%f m;     Read CWT-WLS depth sensor. Ok=%d, depth=%f m; Updating v_bat to N4AIA04 V1 Updating v_bat to N4AIA04 V2     Read temp sensor. Ok=%d, temp=%f degC;     Read inflow pulse sensor. Count=%ld; accm     Skipping in-flow reading due to low flow rate     Read inflow pulse sensor failed.     Read outflow pulse sensor. Count=%ld;     Skipping out-flow reading due to low flow rate     Read outflow pulse sensor failed. Failed to access modbus port EWC hex truncation: 
            From EWC: %s; Unknown EWC message: {Saw %x %x ... %x}   ewcWrite_GetStatusReply given invalid year %04d   ewcWrite_HeathCheckEvent given invalid year %04d   ewcWrite_DispenseLimitEvent given invalid year %04d  _BOOT Did not recognise EWC version string '%s'. An OTA may be triggered. 
            ERROR: startEsp32RemoteControlOta called with an invalid source

            ERROR: startEsp32RemoteControlOta must be called as a task! (bleOtaTask == nullptr)
             Bluetooth client wants to push new firmware: ESP32 version=%ld

            ERROR: startEwcRemoteControlOta called with an invalid source
             Bluetooth client wants to push new firmware: EWC version=%ld
            EWC OTA Complete *** EWC OTA FAILED *** RC OTA is already running BleOtaEwc BleOtaEsp Could not start RC-OTA task Ending modem test mode Modem released Preparing modem for test. Please wait. Test mode ready FIRM Firmware version:      Built:  Apr 10 2026 15:39:07 UTC
            Modem time:  Time not set
            No sync since power-on.
            Last sync time:  No sync yet
            Time since power-up:  %lld sec %lld min %lld hours %lld days a very long time Core 0:  Core 1:  Power-up count:  Memory:  %ld kB Low tide:  Memory CRITICAL! SIM User not set
            SIM User:  SIM Password not set
            SIM Password:  SIM PIN not set
            SIM PIN:  IMEI:  IMEI not known
            ICCID:  ICCID not yet read
            Asset ID:  Asset ID not known
            0x%08lx EWC ID:  EWC ID not known
            No connected server
            Last connected server:  No last network provider
            Last network provider:  Last EWC OTA:  No EWC OTA recorded
            Engineer time remaining:  Eng mode not read
            Bad 'sset' key length Could not read 'sset' key Could not read 'sset' value Log in required (sset:EwcSeed) 
            EWC Seed updated apn Log in required (sset:apn) 
            Old APN:  ; New APN:  
            Network APN updated APN 
            Network APN truncated user Log in required (sset:user) 
            Old username:  ; New username:  
            SIM username updated 
            SIM username truncated pass Log in required (sset:pass) 
            Old password:  ; New password:  
            SIM password updated 
            SIM password truncated pin Log in required (sset:pin) 
            Old PIN:  ; New PIN:  
            SIM PIN updated PIN 
            SIM PIN truncated syncLed 
            LED reset Log in required (sset:name) 
            Updated asset name NAME Failed to set Asset Name LOGS Logs on Logs off get Log in required (sset:restart) REBOOT REQUESTED. BLUETOOTH WILL DISCONNECT. ENGINEER MODE CANCELLED. Log in required (sset:sync) modemTrace Key in 'sset' was not a known setting Log in required (rcon) Bad 'rcon' key length FAULT Could not read 'rcon' key Start 
            Sync off, EWC in manual mode Stop 
            Sync on, EWC in auto mode ModemCtrl kill 
            Aux power hard reset Resetting modem on Bluetooth request

            Modem reset by hardware Waking modem on Bluetooth request

            Modem restarted by hardware Unknown modem command 
            Data session up 
            Failed to enable data 
            Data session down Unknown data command NetRq Time-out waiting for reply Failed to send data 
            OTA recovery data reset Unknown OTA command Restarting device detection task 
            Rescanning modbus network read 
            Reading devices on modbus network Unknown Modbus command 

            -- Starting EWC task --

            waking normal Unexpected EWC status: %d Unknown EWC control command Key in 'rcon' was not a known command Log in required (lset) Failed to read 'lset' key. Ignoring 'lset' key '%s' failed Invalid setting in 'lset' block. Ignoring: MTU test contained:  Log in required (mdmc) Could not read command Not enough memory available Modem failed to respond Engineer mode ended Invalid wake time Engineer mode active Log in required (bind) Failed to read asset assignment Removed asset binding Old binding = %ld;
            BIND Invalid asset assignment Changed asset binding Old binding = %ld; New binding = %lld;
            Log in required (ecmd) Qd EWC-CMD Log in required (push) Bad OTA version ESP32 OTA is already in progress EWC OTA is already in progress Finishing sync and preparing for EWC OTA
            Please wait... Failed to trigger OTA Finishing sync and preparing for ESP32 OTA
            Please wait... Bad OTA protocol IMEI not known. Will try to sync ICCID not known. Will try to sync Failed to read user ID. Remote control reply, but no control device known BUF-BAD RC ledger entry:  EWC reply:  Reply truncated EWC-REPLY EWC-REPLY insert failed Reply: Unexpected remote-control target: %d Bad protocol version sset Bad OTA versions Bad ESP32 version Bad EWC version ecmd unexpected '%s', ignoring 
            WARN: Bluetooth message probably overran 
            """;

        Console.WriteLine($"Input size: {example.Length}");

        var words = example
                    .ReplaceAsciiCompatible()
                    .CamelCaseToWords()
                    .ReplaceByMatch(" ", c => c != ' ' && c != '%' && !char.IsLetter(c))
                    .Replace("%", " %")
                    .NormaliseWhitespace()
                    .ToLowerInvariant()
                    .Split([' '], StringSplitOptions.RemoveEmptyEntries);

        var maxLength   = 0;
        var longestWord = "";
        var unique      = new Dictionary<string, int>();
        foreach (var word in words)
        {
            if (unique.ContainsKey(word)) unique[word]++;
            else unique.Add(word, 1);

            if (word.Length > maxLength)
            {
                maxLength = word.Length;
                longestWord = word;
            }
        }

        Console.WriteLine($"Maximum word length: {maxLength} '{longestWord}'");
        Console.WriteLine($"Found {unique.Count} unique words from {words.Length} input\r\n");
        foreach (var word in unique.OrderByDescending(kvp=>kvp.Value))
        {
            if (word.Value < 50) break; // only show top hits
            Console.WriteLine($"{word.Key} ({word.Value})");
        }

        // Order by word, prefix by overlap
        var wordsOrdered = unique.Keys.OrderBy(k => k).ToList();

        var dicGuessSize = wordsOrdered[0].Length;
        Console.WriteLine($"0:{wordsOrdered[0]}");


        for (int i = 1; i < wordsOrdered.Count; i++)
        {
            var overlap = wordsOrdered[i].InitialOverlapWith(wordsOrdered[i - 1]);
            Console.WriteLine($"{overlap}:{wordsOrdered[i].Substring(overlap)}");

            dicGuessSize += 1 + (wordsOrdered[i].Length - overlap);
        }


        Console.WriteLine($"Dictionary size: {dicGuessSize}");
    }
}